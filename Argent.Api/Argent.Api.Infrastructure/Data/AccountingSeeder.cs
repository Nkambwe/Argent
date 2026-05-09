using Argent.Api.Domain.Entities.Accounting;
using Argent.Api.Domain.Entities.Accounting.Currencies;
using Argent.Api.Domain.Entities.Accounting.Documents;
using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Entities.Accounting.Vouchers;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Data {
    /// <summary>
    /// Seeds the accounting reference data required before any transaction can be posted:
    ///   1. Currencies (world currencies — ~80 records)
    ///   2. Account References (analytical dimensions — 8 system references)
    ///   3. Folio Types and Folios (transaction narration templates)
    ///   4. Chart of Accounts (SACCO default COA with headers and ledger accounts)
    ///   5. Journal Types (6 system journal types)
    ///   6. Voucher Types (6 system voucher types)
    ///   7. Series Numbers (document numbering engine — branch-agnostic defaults)
    ///
    /// All operations are idempotent — safe to run on every startup.
    /// </summary>
    public static class AccountingSeeder {
        public static async Task SeedAsync(AppDataContext context, IServiceLogger logger) {
            logger.Channel = "ACCOUNTING-SEED";
            logger.Log("Running accounting seeder...", "SEED");

            await SeedCurrenciesAsync(context, logger);
            await SeedAccountReferencesAsync(context, logger);
            await SeedFolioTypesAsync(context, logger);
            await SeedChartOfAccountsAsync(context, logger);
            await SeedJournalTypesAsync(context, logger);
            await SeedVoucherTypesAsync(context, logger);
            await SeedDocumentTypesAsync(context, logger);

            logger.Log("Accounting seeding complete.", "SEED");
        }

        #region Currencies

        private static readonly (string Country, string Name, string Code, string Symbol, string SmallUnit, int Precision)[] WorldCurrencies =
        [
            ("Afghanistan","Afghan Afghani","AFN", "؋","Pul",2),
            ("Albania","Albanian Lek","ALL", "L","Qindarkë",2),
            ("Algeria","Algerian Dinar","DZD", "د.ج","Santeem",2),
            ("Angola","Angolan Kwanza","AOA", "Kz","Cêntimo",2),
            ("Australia","Australian Dollar","AUD","$","Cent",2),
            ("Bangladesh","Bangladeshi Taka","BDT","৳","Poisha",2),
            ("Belarus","Belarusian Ruble","BYN","Br","Kapyeyka",2),
            ("Benin","West African CFA Franc","XOF", "Fr","Centime", 0),
            ("Botswana","Botswana Pula","BWP", "P", "Thebe",2),
            ("Brazil","Brazilian Real","BRL", "R$","Centavo",2),
            ("Bulgaria","Bulgarian Lev","BGN", "лв.","Stotinka",2),
            ("Burundi","Burundian Franc","BIF","Fr","Centime",0),
            ("Cambodia","Cambodian Riel", "KHR", "៛","Sen",2),
            ("Cameroon","Central African CFA Franc","XAF", "Fr","Centime",0),
            ("Canada","Canadian Dollar","CAD", "$","Cent",2),
            ("Cape Verde","Cape Verdean Escudo","CVE", "Esc",  "Centavo",2),
            ("Central African Republic","Central African CFA Franc","XAF", "Fr","Centime",0),
            ("China","Chinese Yuan","CNY", "¥","Jiao",2),
            ("Comoros","Comorian Franc","KMF", "Fr","Centime",0),
            ("Congo DR","Congolese Franc","CDF", "Fr",   "Centime",2),
            ("Congo Republic","Central African CFA Franc","XAF", "Fr","Centime",0),
            ("Costa Rica","Costa Rican Colón","CRC", "₡","Céntimo",2),
            ("Denmark","Danish Krone","DKK","kr","Øre", 2),
            ("Djibouti","Djiboutian Franc","DJF", "Fr","Centime", 0),
            ("Dominican Republic","Dominican Peso","DOP", "RD$","Centavo",2),
            ("Egypt","Egyptian Pound","EGP","ج.م","Piastre",2),
            ("Eritrea","Eritrean Nakfa","ERN","Nfk","Cent",2),
            ("Ethiopia","Ethiopian Birr","ETB", "Br", "Santim",2),
            ("European Union","Euro","EUR", "€","Cent",2),
            ("Gabon","Central African CFA Franc","XAF", "Fr", "Centime",0),
            ("Gambia","Gambian Dalasi","GMD", "D","Butut",2),
            ("Georgia","Georgian Lari","GEL", "₾","Tetri",2),
            ("Ghana","Ghanaian Cedi","GHS", "₵","Pesewa",2),
            ("Guinea","Guinean Franc","GNF", "Fr","Centime",0),
            ("Guinea-Bissau","West African CFA Franc","XOF", "Fr","Centime",0),
            ("India","Indian Rupee","INR", "₹","Paisa",2),
            ("Indonesia", "Indonesian Rupiah","IDR", "Rp","Sen", 2),
            ("Japan","Japanese Yen", "JPY", "¥", "-",0),
            ("Kenya","Kenyan Shilling","KES", "Ksh",  "Cent",2),
            ("Lesotho","Lesotho Loti","LSL", "L","Sente",2),
            ("Liberia","Liberian Dollar","LRD", "$","Cent",2),
            ("Libya","Libyan Dinar", "LYD","ل.د","Dirham",3),
            ("Madagascar","Malagasy Ariary","MGA", "Ar","Iraimbilanja", 2),
            ("Malawi","Malawian Kwacha","MWK", "MK","Tambala",2),
            ("Mali","West African CFA Franc","XOF", "Fr","Centime", 0),
            ("Mauritania","Mauritanian Ouguiya","MRU", "UM",   "Khoums",2),
            ("Mozambique","Mozambican Metical","MZN", "MT","Centavo",2),
            ("Namibia","Namibian Dollar","NAD", "$","Cent", 2),
            ("New Zealand","New Zealand Dollar","NZD", "$","Cent",2),
            ("Niger","West African CFA Franc","XOF","Fr","Centime",0),
            ("Nigeria","Nigerian Naira","NGN","₦","Kobo",2),
            ("Norway", "Norwegian Krone","NOK", "kr","Øre",2),
            ("Poland","Polish Złoty","PLN","zł","Grosz",2),
            ("Qatar","Qatari Riyal","QAR","ر.ق","Dirham",2),
            ("Romania","Romanian Leu","RON","lei","Ban",2),
            ("Russia","Russian Ruble","RUB", "₽","Kopek",2),
            ("Rwanda","Rwandan Franc","RWF", "Fr","Centime",0),
            ("Saudi Arabia","Saudi Riyal","SAR", "ر.س", "Halala",2),
            ("Somalia","Somali Shilling","SOS", "Sh","Cent",2),
            ("South Africa","South African Rand","ZAR", "R","Cent",2),
            ("South Sudan","South Sudanese Pound","SSP", "£","Piaster",2),
            ("Sri Lanka","Sri Lankan Rupee","LKR", "Rs","Cent",2),
            ("Sudan","Sudanese Pound", "SDG", "£","Piastre",2),
            ("Sweden","Swedish Krona","SEK", "kr","Öre",2),
            ("Switzerland","Swiss Franc","CHF", "Fr","Centime",2),
            ("Tanzania","Tanzanian Shilling","TZS", "Sh","Cent",2),
            ("Togo","West African CFA Franc","XOF", "Fr","Centime",0),
            ("Tunisia","Tunisian Dinar","TND", "د.ت","Millime",3),
            ("Turkey","Turkish Lira","TRY", "₺","Kuruş",2),
            ("Uganda","Uganda Shilling","UGX", "USh","Cent",0),
            ("Ukraine","Ukrainian Hryvnia","UAH", "₴","Kopiyka",2),
            ("United Arab Emirates","UAE Dirham","AED", "د.إ",  "Fils",2),
            ("United Kingdom","Pound Sterling","GBP", "£","Penny",2),
            ("United States", "US Dollar","USD", "$","Cent",4),
            ("Uzbekistan","Uzbekistani Soʻm","UZS", "Sʻ","Tiyin",2),
            ("Zambia","Zambian Kwacha","ZMW", "ZK","Ngwee",2),
            ("Zimbabwe","Zimbabwean Dollar","ZWL", "$","Cent",2),
        ];

        private static async Task SeedCurrenciesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.Currencies
                .Select(c => c.Code)
                .ToHashSetAsync();

            var toInsert = WorldCurrencies
                .Where(c => !existing.Contains(c.Code))
                // Deduplicate by code (XAF/XOF appear for multiple countries)
                .GroupBy(c => c.Code)
                .Select(g => g.First())
                .Select(c => new Currency
                {
                    Code = c.Code,
                    Name = c.Name,
                    Symbol = c.Symbol,
                    SmallUnit = c.SmallUnit,
                    Precision = c.Precision,
                    Round = 0,
                    Country = c.Country,
                    IsBaseCurrency = false,
                    IsSystem = true,
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.Currencies.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} currency(ies).", "SEED");
            }
            else
                logger.Log("Currencies already seeded.", "SEED");
        }

        #endregion

        #region Account References - Analytical Dimensions

        private static readonly (string Code, string Name)[] SystemReferences =
        [
            ("CHRG", "Charges"),
            ("COST", "Cost Centers"),
            ("REVS", "Revenue Centers"),
            ("PROD", "Products"),
            ("VENS", "Vendors"),
            ("CUST", "Customers"),
            ("BUSL", "Business Lines"),
            ("AREA", "Areas"),
        ];

        private static async Task SeedAccountReferencesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.AccountReferences
                .Select(r => r.Code)
                .ToHashSetAsync();

            var toInsert = SystemReferences
                .Where(r => !existing.Contains(r.Code))
                .Select(r => new AccountReference {
                    Code = r.Code,
                    Series = r.Code,
                    Name = r.Name,
                    Active = false,  
                    IsSystem = true,
                    CreatedBy = "system"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.AccountReferences.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} account reference(s).", "SEED");
            }
            else
                logger.Log("Account references already seeded.", "SEED");
        }

        #endregion

        #region Folio Types and Folios

        private static readonly (string Code, string Description, (string Code, string Particulars)[] Folios)[] FolioTypes =
        [
            ("FIXASS","Fixed Asset Folios",
            [
                ("FIXASS01","Purchase of fixed asset"),
                ("FIXASS02","Accumulated Depreciation"),
                ("FIXASS03","Asset Repairs and Maintenance"),
                ("FIXASS04","Asset Revaluation"),
                ("FIXASS05","Asset Disposal"),
                ("FIXASS06","Sale of fixed asset")
            ]),
            ("FOCASH","Cash and Bank Folios", 
            [
                ("FOCASH01","Cash transaction"),
                ("FOCASH02","Cash transfer"),
                ("FOCASH03","Cash transfer (Contra)"),
                ("FOCASH04","Cash Receipt"),
                ("FOCASH05","Cash Payment"),
                ("FOCASH06","Bank withdrawal"),
                ("FOCASH07","Bank Payment"),
                ("FOCASH08","Bank transfer"),
                ("FOCASH09","Bank deposit"),
                ("FOCASH10","Credit card transaction"),
                ("FOCASH11","Transaction Reversal"),
                ("FOCASH12","Deleted cash transaction"),
                ("FOCASH13","Deleted bank transaction"),
                ("FOCASH14","Deleted credit card transaction"),
                ("FOCASH15","Reversed credit card transaction"),
                ("FOCASH16","Reversed bank transaction")
            ]),
            ("INTASS","Intangible Asset Folios",
            [
                ("INTASS01","Acquisition of intangible asset"),
                ("INTASS02","Accumulated Amortization")
            ]),
            ("DEPEXP","Depreciation Expense", 
            [
                ("DEPEXP01","Depreciation Expense")
            ]),
            ("AMOEXP", "Amortization Expense", 
            [
                ("AMOEXP01","Amortization Expense")
            ]),
            ("GENEXP","General Expenses", 
            [
                ("GENEXP01","General expense payment"),
                ("GENEXP02","Utility payment")
            ]),
            ("MGTEXP", "Managerial Expenses",
            [
                ("MGTEXP01","General meeting expenses"),
                ("MGTEXP02","Board meeting expenses")
            ]),
            ("FINEXP", "Financial Expenses", 
            [
                ("FINEXP01","Payment on legal fees"),
                ("FINEXP02","Bank charges"),
                ("FINEXP03","Interest expense")
            ]),
            ("MKTEXP","Marketing Expenses", 
            [
                ("MKTEXP01","Advertisement expenses"),
                ("MKTEXP02","Printing expenses"),
                ("MKTEXP03","Promotional expenses")
            ]),
            ("SAVOPS","Savings Operations",
            [
                ("SAVOPS01","Savings deposit"),
                ("SAVOPS02","Savings withdrawal"),
                ("SAVOPS03","Savings interest credit"),
                ("SAVOPS04","Savings account closure"),
                ("SAVOPS05","Compulsory savings deduction")
            ]),
            ("LOANOP","Loan Operations",
            [
                ("LOANOP01","Loan disbursement"),
                ("LOANOP02","Loan repayment — principal"),
                ("LOANOP03","Loan repayment — interest"),
                ("LOANOP04","Loan repayment — fees"),
                ("LOANOP05","Loan write-off"),
                ("LOANOP06","Loan recovery"),
                ("LOANOP07","Penalty charge")
            ]),
            ("SHROPS","Share Operations",
            [
                ("SHROPS01","Share purchase"),
                ("SHROPS02","Share redemption"),
                ("SHROPS03","Dividend payment")
            ]),
        ];

        private static async Task SeedFolioTypesAsync(AppDataContext context, IServiceLogger logger) {
            var existingCodes = await context.FolioTypes
                .Select(ft => ft.Code)
                .ToHashSetAsync();

            var count = 0;
            foreach (var (typeCode, typeDesc, folios) in FolioTypes) {
                if (existingCodes.Contains(typeCode)) continue;

                var folioType = new FolioType
                {
                    Code = typeCode,
                    TypeName = typeDesc,
                    CreatedBy = "system",
                    Folios = [.. folios.Select(f => new Folio
                    {
                        Code = f.Code,
                        Particulars = f.Particulars,
                        CreatedBy = "system"
                    })]
                };

                await context.FolioTypes.AddAsync(folioType);
                count++;
            }

            if (count > 0) {
                await context.SaveChangesAsync();
                logger.Log($"Seeded {count} folio type(s).", "SEED");
            }
            else
                logger.Log("Folio types already seeded.", "SEED");
        }

        #endregion

        #region Chart of Accounts

        private static async Task SeedChartOfAccountsAsync(AppDataContext context, IServiceLogger logger) {
            if (await context.AccountsCharts.AnyAsync(c => c.ChartName == "ARGENT_DEFAULT")) {
                logger.Log("Default chart of accounts already seeded.", "SEED");
                return;
            }

            var chart = new AccountsChart {
                ChartName = "ARGENT_DEFAULT",
                Description = "Argent standard chart of accounts — Uganda / East Africa",
                ChartType = ChartType.USUK,
                CreatedBy = "SYSTEM"
            };

            await context.AccountsCharts.AddAsync(chart);
            await context.SaveChangesAsync();

            var headers = BuildSaccoCoaHeaders(chart.Id);
            await context.LedgerAccountHeaders.AddRangeAsync(headers);
            await context.SaveChangesAsync();

            // Ledger accounts reference header IDs — headers must be saved first
            var accounts = BuildSaccoCoaAccounts(chart.Id, headers);
            await context.LedgerAccounts.AddRangeAsync(accounts);

            // Total labels
            var totals = BuildSaccoCoaTotals(headers);
            await context.LedgerAccountTotals.AddRangeAsync(totals);

            await context.SaveChangesAsync();
            logger.Log($"Seeded default SACCO chart of accounts: " +
                $"{headers.Count} headers, {accounts.Count} accounts, {totals.Count} totals.",
                "SEED");
        }

        private static List<LedgerAccountHeader> BuildSaccoCoaHeaders(long chartId) {
            static LedgerAccountHeader H(string number, string name, string parent,
                AccountClassification cls, AccountCategory cat, AccountNature nature,
                long groupIdx, long ledgerIdx) => new() {
                    LedgerNumber = number,
                    LedgerName = name,
                    ParentHeader = parent,
                    AccountClassification = cls,
                    AccountCategory = cat,
                    AccountNature = nature,
                    GroupIndex = groupIdx,
                    LedgerIndex = ledgerIdx,
                    CreatedBy = "system"
                };

            return
            [
                #region Balance Sheet
                H("9100000","BALANCE SHEET","", AccountClassification.Label,AccountCategory.None,AccountNature.Balance, 9100000, 9100000),
                H("1100000","ASSETS","9100000", AccountClassification.Asset,AccountCategory.Category,AccountNature.Balance, 1100000, 1100000),

                // Fixed Assets
                H("1110000","FIXED ASSETS","1100000", AccountClassification.Asset,AccountCategory.SubCategory, AccountNature.Balance, 1110000, 1110000),
                H("1111000","MOTOR VEHICLES","1110000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1111000, 1111000),
                H("1112000","FIXTURES AND FITTINGS","1110000",AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1112000, 1112000),
                H("1113000","OFFICE EQUIPMENT", "1110000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1113000, 1113000),
                H("1114000","IT EQUIPMENT","1110000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1114000, 1114000),

                // Current Assets
                H("1120000","CURRENT ASSETS","1100000", AccountClassification.Asset,AccountCategory.SubCategory, AccountNature.Balance, 1120000, 1120000),
                H("1121000","CASH AT HAND","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1121000, 1121000),
                H("1122000","CASH AT BANK","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1122000, 1122000),
                H("1123000","STOCK AND INVENTORY","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1123000, 1123000),
                H("1124000","PRINCIPAL OUTSTANDING","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1124000, 1124000),
                H("1125000","LOANS WRITTEN-OFF","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1125000, 1125000),
                H("1126000","ACCOUNTS RECEIVABLE","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1126000, 1126000),
                H("1127000","CHEQUES SUSPENSE","1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1127000, 1127000),
                H("1128000","INTER-BRANCH RECEIVABLES","1120000",AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1128000, 1128000),
                H("1129000","OTHER CURRENT ASSETS", "1120000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1129000, 1129000),

                // Intangible Assets
                H("1130000","INTANGIBLE ASSETS","1100000", AccountClassification.Asset,AccountCategory.SubCategory, AccountNature.Balance, 1130000, 1130000),
                H("1131000","SOFTWARE","1130000", AccountClassification.Asset,AccountCategory.Header,AccountNature.Balance, 1131000, 1131000),

                // Other Assets
                H("1190000","OTHER ASSETS","1100000", AccountClassification.Asset, AccountCategory.SubCategory, AccountNature.Balance, 1190000, 1190000),
                H("1191000","MOBILE MONEY FLOAT","1190000", AccountClassification.Asset, AccountCategory.Header, AccountNature.Balance, 1191000, 1191000),

                //..equity and liabilities
                H("2000000","EQUITY AND LIABILITIES","9100000",AccountClassification.Label,AccountCategory.Category,AccountNature.Balance, 2000000, 2000000),
            
                //..liabilities   
                H("2100000","LIABILITIES","2000000", AccountClassification.Label,AccountCategory.Category,AccountNature.Balance, 2100000, 2100000),
                H("2110000","SAVINGS DEPOSIT","2100000", AccountClassification.Liability,AccountCategory.SubCategory,AccountNature.Balance, 2110000, 2110000),
                H("2111000","GENERAL SAVINGS","2110000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2111000, 2111000),
                H("2112000","INDIVIDUAL SAVINGS","2110000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2112000, 2112000),
                H("2113000","GROUP SAVINGS","2110000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2113000, 2113000),
                H("2114000","BUSINESS SAVINGS","2110000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2114000, 2114000),
                H("2120000","TIME DEPOSITS","2100000", AccountClassification.Liability,AccountCategory.SubCategory,AccountNature.Balance, 2120000, 2120000),
                H("2121000","GENERAL TIME DEPOSITS","2120000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance, 2121000, 2121000),
                H("2140000","PROVISIONS","2100000", AccountClassification.Liability,AccountCategory.SubCategory,AccountNature.Balance, 2140000, 2140000),
                H("2141000","LOAN LOSS PROVISIONS","2140000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance, 2141000, 2141000),
                H("2150000","ACCRUALS","2100000", AccountClassification.Liability,AccountCategory.SubCategory,AccountNature.Balance, 2150000, 2150000),
                H("2151000","ACCRUED INTEREST","2150000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2151000, 2151000),
                H("2152000","OTHER ACCRUALS","2150000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2152000, 2152000),
                H("2153000","PAYROLL LIABILITIES","2150000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2153000, 2153000),
                H("2160000","BORROWED FUNDS","2100000", AccountClassification.Liability,AccountCategory.SubCategory,AccountNature.Balance, 2160000, 2160000),
                H("2161000","LONG-TERM BORROWINGS", "2160000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2161000,2161000),
                H("2162000","SHORT-TERM BORROWINGS","2160000", AccountClassification.Liability,AccountCategory.Header,AccountNature.Balance,2162000,2162000),

                //..equity
                H("2200000","EQUITY", "2000000", AccountClassification.Equity,AccountCategory.Category,AccountNature.Balance, 2200000, 2200000),
                H("2210000","SHARE CAPITAL", "2200000", AccountClassification.Equity,AccountCategory.SubCategory,AccountNature.Balance, 2210000, 2210000),
                H("2211000","CONTRIBUTED CAPITAL","2210000", AccountClassification.Equity,AccountCategory.Header,AccountNature.Balance, 2211000, 2211000),
                H("2212000","DONATIONS AND GRANTS","2210000", AccountClassification.Equity,AccountCategory.Header,AccountNature.Balance, 2212000, 2212000),
                H("2213000","CAPITAL RESERVES","2210000", AccountClassification.Equity,AccountCategory.Header,AccountNature.Balance, 2213000, 2213000),
                H("2214000","CAPITAL GAINS/LOSSES", "2210000", AccountClassification.Equity,AccountCategory.Header,AccountNature.Balance, 2214000, 2214000),
                H("2215000","ACCUMULATED FUNDS","2210000", AccountClassification.Equity,AccountCategory.Header,AccountNature.Balance, 2215000, 2215000),

                #endregion

                #region income Statement
                //..incomes
                H("3000000","INCOME STATEMENT","", AccountClassification.Label,AccountCategory.None,AccountNature.Income,3000000, 3000000),
                H("3100000","REVENUES","3000000",AccountClassification.Revenue,AccountCategory.Category,AccountNature.Income,3100000,3100000),
                H("3110000","REVENUE FROM LOANS","3100000",AccountClassification.Revenue,AccountCategory.SubCategory,AccountNature.Income,3110000,3110000),
                H("3111000","INTEREST INCOME","3110000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3111000,3111000),
                H("3112000","LOAN FEE INCOME","3110000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3112000,3112000),
                H("3113000","COMMISSION INCOME","3110000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3113000,3113000),
                H("3120000","SAVINGS INCOME","3100000",AccountClassification.Revenue,AccountCategory.SubCategory,AccountNature.Income,3120000,3120000),
                H("3121000","SAVINGS FEE INCOME","3120000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3121000,3121000),
                H("3130000","MOBILE MONEY INCOME","3100000",AccountClassification.Revenue,AccountCategory.SubCategory,AccountNature.Income,3130000,3130000),
                H("3131000","MOBILE MONEY COMMISSION","3130000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3131000,3131000),
                H("3132000","BANKING AGENCY COMMISSION","3130000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3132000,3132000),
                H("3140000","OTHER INCOME","3100000",AccountClassification.Revenue,AccountCategory.SubCategory,AccountNature.Income,3140000,3140000),
                H("3141000","BANK INTEREST","3140000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3141000,3141000),
                H("3142000","MISCELLANEOUS INCOME", "3140000",AccountClassification.Revenue,AccountCategory.Header,AccountNature.Income,3142000,3142000),
               
                //..expenses  
                H("4000000","EXPENSES","3000000",AccountClassification.Expense,AccountCategory.Category,AccountNature.Income,4000000,4000000),
                H("4100000","ADMIN EXPENSES","4000000",AccountClassification.Expense,AccountCategory.SubCategory,AccountNature.Income,4100000,4100000),
                H("4110000","MEETING EXPENSES","4100000",AccountClassification.Expense,AccountCategory.Header, AccountNature.Income,4110000,4110000),
                H("4200000","MARKETING EXPENSES", "4000000",AccountClassification.Expense,AccountCategory.SubCategory,AccountNature.Income,4200000,4200000),
                H("4210000","MARKETING","4200000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4210000,4210000),
                H("4220000","ADVERTISING","4200000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4220000,4220000),
                H("4300000","OPERATION EXPENSES","4000000",AccountClassification.Expense,AccountCategory.SubCategory,AccountNature.Income,4300000,4300000),
                H("4310000","OFFICE EXPENSES","4300000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4310000,4310000),
                H("4320000","OTHER OPERATIONS","4300000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4320000,4320000),
                H("4400000","FINANCIAL EXPENSES","4000000",AccountClassification.Expense,AccountCategory.SubCategory,AccountNature.Income,4400000,4400000),
                H("4410000","BANK CHARGES","4400000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4410000,4410000),
                H("4420000","LICENSES","4400000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4420000,4420000),
                H("4500000","PAYROLL EXPENSES",     "4000000",AccountClassification.Expense,AccountCategory.SubCategory,AccountNature.Income,4500000,4500000),
                H("4510000","SALARIES", "4500000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4510000,4510000),
                H("4520000","EMPLOYEE BENEFITS","4500000",AccountClassification.Expense,AccountCategory.Header,AccountNature.Income,4520000,4520000),
                #endregion
            ];
        }

        private static List<LedgerAccount> BuildSaccoCoaAccounts(long chartId, List<LedgerAccountHeader> headers) {
            var headerMap = headers.ToDictionary(h => h.LedgerNumber, h => h.Id);

            static LedgerAccount A(long chartId, Dictionary<string, long> map,
                string headerNumber, string number, string name,
                AccountClassification cls, NormalBalance balance, AccountNature nature)
                => new()
                {
                    AccountsChartId = chartId,
                    LedgerAccountHeaderId = map[headerNumber],
                    LedgerNumber = number,
                    LedgerName = name,
                    AccountClassification = cls,
                    AccountCategory = AccountCategory.Ledger,
                    AccountNature = nature,
                    NormalBalance = balance,
                    PostingType = PostingType.Both,
                    AllowManualPosting = true,
                    ShowParticulars = true,
                    Suspended = false,
                    Balance = 0.00m,
                    GroupIndex = long.Parse(headerNumber),
                    LedgerIndex = long.Parse(number),
                    CreatedBy = "SYSTEM"
                };

            var m = headerMap;
            var c = chartId;

            return
            [
                // Motor Vehicles
                A(c,m,"1111000","1111001","Delivery Vans",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1111000","1111002","Pickup Trucks",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1111000","1111003","Managerial Cars",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                // Fixtures
                A(c,m,"1112000","1112001","Furniture", AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1112000","1112002","Office Curtains",AccountClassification.Asset,NormalBalance.Debit,  AccountNature.Balance),
                A(c,m,"1112000","1112003","Boardroom Furniture",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                // Office Equipment
                A(c,m,"1113000","1113001","Cleaning Equipment",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1113000","1113002","Printing Machines", AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                // IT Equipment
                A(c,m,"1114000","1114001","Printers", AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1114000","1114002","Computers",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1114000","1114003","Servers",  AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                // Cash
                A(c,m,"1121000","1121001","Petty Cash",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1121000","1121002","Cash Sales",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1121000","1121003","Cash For Deposit",AccountClassification.Asset,NormalBalance.Debit,  AccountNature.Balance),
                // Bank
                A(c,m,"1122000","1122001","Reserve Accounts",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1122000","1122002","Checking Account",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1122000","1122003","Foreign Exchange Account",AccountClassification.Asset,NormalBalance.Debit,  AccountNature.Balance),
                // Stock
                A(c,m,"1123000","1123001","Office Stationery",AccountClassification.Asset,NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1123000","1123002","Card Stock",AccountClassification.Asset,NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1123000","1123003","Promotional Items",AccountClassification.Asset,NormalBalance.Debit,  AccountNature.Balance),
                // Principal Outstanding
                A(c,m,"1124000","1124001","Principal Outstanding (Individuals)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1124000","1124002","Principal Outstanding (Groups)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1124000","1124003","Principal Outstanding (Businesses)", AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1124000","1124004","Principal Outstanding (Overdrafts)", AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                // Loans Written-off
                A(c,m,"1125000","1125001","Loans Written-off (Individuals)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1125000","1125002","Loans Written-off (Groups)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1125000","1125003","Loans Written-off (Businesses)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                // Receivables
                A(c,m,"1126000","1126001","Accrued Interest Due (Individual Loans)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1126000","1126002","Accrued Interest Due (Group Loans)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1126000","1126003","Accrued Interest Due (Business Loans)",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1126000","1126004","Trade Accounts Receivable",AccountClassification.Asset, NormalBalance.Debit, AccountNature.Balance),
                // Cheque Suspense
                A(c,m,"1127000","1127001","Unpresented Cheques",AccountClassification.Asset,NormalBalance.Debit,  AccountNature.Balance),
                // Inter-branch
                A(c,m,"1128000","1128001","Inter-branch Transfer Receivable",AccountClassification.Asset,NormalBalance.Debit, AccountNature.Balance),
                A(c,m,"1128000","1128002","Other Inter-branch Receivables",  AccountClassification.Asset,NormalBalance.Debit, AccountNature.Balance),
                // Prepayments
                A(c,m,"1129000","1129001","Prepaid Rent",AccountClassification.Asset, NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1129000","1129002","Prepaid Licenses",AccountClassification.Asset, NormalBalance.Debit,AccountNature.Balance),
                // Software
                A(c,m,"1131000","1131001","SACCO Management Software",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1131000","1131002","Accounting Software",AccountClassification.Asset, NormalBalance.Debit,AccountNature.Balance),
                // Mobile Money Float
                A(c,m,"1191000","1191001","MTN Mobile Money Float",AccountClassification.Asset, NormalBalance.Debit,AccountNature.Balance),
                A(c,m,"1191000","1191002","Airtel Mobile Money Float",AccountClassification.Asset,NormalBalance.Debit,AccountNature.Balance),

                //..liabilities
                A(c,m,"2111000","2111001","Savings Deposits", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2112000","2112001","Loan Guarantee Savings (Individuals)",AccountClassification.Liability, NormalBalance.Credit,AccountNature.Balance),
                A(c,m,"2112000","2112002","Compulsory Savings (Individuals)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2113000","2113001","Loan Guarantee Savings (Groups)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2113000","2113002","Compulsory Savings (Groups)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2114000","2114001","Loan Guarantee Savings (Businesses)", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2114000","2114002","Compulsory Savings (Businesses)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2121000","2121001","Time Deposit Savings",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2121000","2121002","Interest Due (Time Deposit)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2141000","2141001","Provision — Loan Loss (Individuals)", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2141000","2141002","Provision — Loan Loss (Groups)", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2141000","2141003","Provision — Loan Loss (Businesses)",  AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2151000","2151001","Accrued Interest (Individual Savings)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2151000","2151002","Accrued Interest (Group Savings)",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2151000","2151003","Accrued Interest (Business Savings)", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2151000","2151004","Accrued Interest (Time Deposit)", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2152000","2152001","Accrued Withholding Tax",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2152000","2152002","Stamp Duty Payable",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2152000","2152003","Dividends Due",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2152000","2152004","Trade Accounts Payable", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2153000","2153001","PAYE Payable",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2153000","2153002","Salaries Payable",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2153000","2153003","NSSF Payable",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2153000","2153004","Incentives Payable",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2161000","2161001","Long-term Bank Loan",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2161000","2161002","Shareholder Loans", AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2162000","2162001","Bank Overdraft",AccountClassification.Liability, NormalBalance.Credit, AccountNature.Balance),

                //..equity
                A(c,m,"2211000","2211001","Share Capital (Members)",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2211000","2211002","Share Capital (Others)",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2211000","2211003","Redeemable Shares (Members)", AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2211000","2211004","Premium Shares",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2212000","2212001","Loan Grants",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2212000","2212002","Operation Funds",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2212000","2212003","Other Funds",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2213000","2213001","General Reserves", AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2213000","2213002","Statutory Reserves",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2213000","2213003","Foreign Currency Reserve",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2214000","2214001","Leasehold Revaluation",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2214000","2214002","Land Revaluation",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2214000","2214003","Foreign Currency Revaluation",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2215000","2215001","Retained Earnings",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2215000","2215002","Profit (Loss) Earned",AccountClassification.Equity, NormalBalance.Credit, AccountNature.Balance),
                A(c,m,"2215000","2215003","Drawings",AccountClassification.Equity, NormalBalance.Debit,  AccountNature.Balance),

                //..revenue
                A(c,m,"3111000","3111001","Interest Received (Individual Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3111000","3111002","Interest Received (Group Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3111000","3111003","Interest Received (Business Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3111000","3111004","Accrued Interest (Individual Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3111000","3111005","Accrued Interest (Group Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3111000","3111006","Accrued Interest (Business Loans)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3112000","3112001","Loan Application Fees",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3112000","3112002","Loan Processing Fees", AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3112000","3112003","Passbook Fees",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3113000","3113001","Loan Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3113000","3113002","Cheque Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3121000","3121001","Account Opening Fees", AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3121000","3121002","Penalty On Time Deposit",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3121000","3121003","Withdrawal Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3131000","3131001","MTN Mobile Money Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3131000","3131002","Airtel Mobile Money Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3132000","3132001","Agent Commission (Centenary Bank)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3132000","3132002","Agent Commission (Equity Bank)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3132000","3132003","Agent Commission (Stanbic Bank)",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3141000","3141001","Interest On Savings Account", AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3141000","3141002","Interest On Foreign Currency",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3142000","3142001","Registration Fees",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3142000","3142002","Money Transfer Commission",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3142000","3142003","Recovery Of Loans Written Off", AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3142000","3142004","Sale Of Stationery",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),
                A(c,m,"3142000","3142005","Rental Income",AccountClassification.Revenue, NormalBalance.Credit, AccountNature.Income),

                //..expenses
                A(c,m,"4110000","4110001","Meeting Expenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4110000","4110002","Board Meeting Expenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4110000","4110003","Board Meeting Allowances",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4210000","4210001","Delivery Expenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4210000","4210002","Promotional Expenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4210000","4210003","Marketing Allowances",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4220000","4220001","Billboard Charges",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4220000","4220002","Printing Expenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4220000","4220003","TV And Radio Advertising", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4310000","4310001","Internet Subscription", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4310000","4310002","Telephone and Airtime", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4310000","4310003","Courier And Postage", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4310000","4310004","Rent",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4310000","4310005","Electricity And Water", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4320000","4320001","Cleaning",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4320000","4320002","Rubbish Collection",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4410000","4410001","Bank Monthly Charges",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4410000","4410002","Bank Statement Charges",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4420000","4420001","Operating Licenses", AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4420000","4420002","Other Licenses",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4510000","4510001","Employee Salaries",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4510000","4510002","Employee Bonus",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4510000","4510003","Contractors",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4520000","4520001","Staff Capacity Building",  AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4520000","4520002","Field Allowances",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4520000","4520003","Medical Reimbursement",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
                A(c,m,"4520000","4520004","NSSF Contribution (Employer)",AccountClassification.Expense, NormalBalance.Debit, AccountNature.Income),
            ];
        }

        private static List<LedgerAccountTotal> BuildSaccoCoaTotals(List<LedgerAccountHeader> headers) {
            var headerMap = headers.ToDictionary(h => h.LedgerNumber, h => h.Id);

            static LedgerAccountTotal T(Dictionary<string, long> map,
                string headerNumber, string number, string name,
                AccountClassification cls, AccountCategory cat, AccountNature nature,
                long groupIdx, long ledgerIdx, string range)
                => new()
                {
                    LedgerAccountHeaderId = map[headerNumber],
                    LedgerNumber = number,
                    LedgerName = name,
                    AccountClassification = cls,
                    AccountCategory = cat,
                    AccountNature = nature,
                    GroupIndex = groupIdx,
                    LedgerIndex = ledgerIdx,
                    TotalRange = range,
                    CreatedBy = "SYSTEM"
                };

            var m = headerMap;
            return
            [
                T(m,"1111000","1111999","TOTAL MOTOR VEHICLES",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1111999,1111999,"1111000"),
                T(m,"1112000","1112999","TOTAL FIXTURES AND FITTINGS",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1112999,1112999,"1112000"),
                T(m,"1113000","1113999","TOTAL OFFICE EQUIPMENT", AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1113999,1113999,"1113000"),
                T(m,"1114000","1114999","TOTAL IT EQUIPMENT",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1114999,1114999,"1114000"),
                T(m,"1110000","1119999","TOTAL FIXED ASSETS",AccountClassification.Asset,AccountCategory.SubTotal,AccountNature.Balance,1119999,1119999,"1110000:1119999"),
                T(m,"1121000","1121999","TOTAL CASH AT HAND",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1121999,1121999,"1121000"),
                T(m,"1122000","1122999","TOTAL CASH AT BANK",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1122999,1122999,"1122000"),
                T(m,"1123000","1123999","TOTAL STOCK AND INVENTORY",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1123999,1123999,"1123000"),
                T(m,"1124000","1124999","TOTAL PRINCIPAL OUTSTANDING",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1124999,1124999,"1124000"),
                T(m,"1125000","1125999","TOTAL LOANS WRITTEN-OFF",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1125999,1125999,"1125000"),
                T(m,"1126000","1126999","TOTAL ACCOUNTS RECEIVABLE",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1126999,1126999,"1126000"),
                T(m,"1127000","1127999","TOTAL CHEQUES SUSPENSE",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1127999,1127999,"1127000"),
                T(m,"1128000","1128999","TOTAL INTER-BRANCH RECEIVABLES",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1128999,1128999,"1128000"),
                T(m,"1129000","1129999","TOTAL OTHER CURRENT ASSETS",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1129999,1129999,"1129000"),
                T(m,"1120000","1129998","TOTAL CURRENT ASSETS",AccountClassification.Asset,AccountCategory.SubTotal,AccountNature.Balance,1129998,1129998,"1121000:1129999"),
                T(m,"1131000","1131999","TOTAL SOFTWARE",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1131999,1131999,"1131000"),
                T(m,"1130000","1139999","TOTAL INTANGIBLE ASSETS", AccountClassification.Asset,AccountCategory.SubTotal,AccountNature.Balance,1139999,1139999,"1130000:1139999"),
                T(m,"1191000","1191999","TOTAL MOBILE MONEY FLOAT",AccountClassification.Asset,AccountCategory.HeaderTotal,AccountNature.Balance,1191999,1191999,"1191000"),
                T(m,"1190000","1199998","TOTAL OTHER ASSETS",AccountClassification.Asset,AccountCategory.SubTotal,AccountNature.Balance,1199998,1199998,"1190000:1199998"),
                T(m,"1100000","1199999","TOTAL ASSETS",AccountClassification.Asset,AccountCategory.CategoryTotal, AccountNature.Balance,1199999,1199999,"1100001:1199999"),

                T(m,"2111000","2111999","TOTAL GENERAL SAVINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2111999,2111999,"2111000"),
                T(m,"2112000","2112999","TOTAL INDIVIDUAL SAVINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2112999,2112999,"2112000"),
                T(m,"2113000","2113999","TOTAL GROUP SAVINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2113999,2113999,"2113000"),
                T(m,"2114000","2114999","TOTAL BUSINESS SAVINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2114999,2114999,"2114000"),
                T(m,"2110000","2119999","TOTAL SAVINGS DEPOSIT", AccountClassification.Liability,AccountCategory.SubTotal,AccountNature.Balance,2119999,2119999,"2110000:2119999"),
                T(m,"2121000","2121999","TOTAL TIME DEPOSITS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2121999,2121999,"2121000"),
                T(m,"2120000","2129999","TOTAL TIME DEPOSITS",AccountClassification.Liability,AccountCategory.SubTotal,AccountNature.Balance,2129999,2129999,"2120000:2129999"),
                T(m,"2141000","2141999","TOTAL LOAN LOSS PROVISIONS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2141999,2141999,"2141000"),
                T(m,"2140000","2149999","TOTAL PROVISIONS",AccountClassification.Liability,AccountCategory.SubTotal,AccountNature.Balance,2149999,2149999,"2140000:2149999"),
                T(m,"2151000","2151999","TOTAL ACCRUED INTEREST",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2151999,2151999,"2151000"),
                T(m,"2152000","2152999","TOTAL OTHER ACCRUALS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2152999,2152999,"2152000"),
                T(m,"2153000","2153999","TOTAL PAYROLL LIABILITIES", AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2153999,2153999,"2153000"),
                T(m,"2150000","2159999","TOTAL ACCRUALS",AccountClassification.Liability,AccountCategory.SubTotal,AccountNature.Balance,2159999,2159999,"2150000:2159999"),
                T(m,"2161000","2161999","TOTAL LONG-TERM BORROWINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2161999,2161999,"2161000"),
                T(m,"2162000","2162999","TOTAL SHORT-TERM BORROWINGS",AccountClassification.Liability,AccountCategory.HeaderTotal,AccountNature.Balance,2162999,2162999,"2162000"),
                T(m,"2160000","2169999","TOTAL BORROWED FUNDS",AccountClassification.Liability,AccountCategory.SubTotal,AccountNature.Balance,2169999,2169999,"2160000:2169999"),
                T(m,"2100000","2199999","TOTAL LIABILITIES",AccountClassification.Liability,AccountCategory.CategoryTotal,AccountNature.Balance,2199999,2199999,"2100001:2199999"),
                T(m,"2211000","2211999","TOTAL CONTRIBUTED CAPITAL",AccountClassification.Equity,AccountCategory.HeaderTotal,AccountNature.Balance,2211999,2211999,"2211000"),
                T(m,"2212000","2212999","TOTAL DONATIONS AND GRANTS",AccountClassification.Equity,AccountCategory.HeaderTotal,AccountNature.Balance,2212999,2212999,"2212000"),
                T(m,"2213000","2213999","TOTAL CAPITAL RESERVES",AccountClassification.Equity,AccountCategory.HeaderTotal,AccountNature.Balance,2213999,2213999,"2213000"),
                T(m,"2214000","2214999","TOTAL CAPITAL GAINS/LOSSES",AccountClassification.Equity,AccountCategory.HeaderTotal,AccountNature.Balance,2214999,2214999,"2214000"),
                T(m,"2215000","2215999","TOTAL ACCUMULATED FUNDS",AccountClassification.Equity,AccountCategory.HeaderTotal,AccountNature.Balance,2215999,2215999,"2215000"),
                T(m,"2210000","2219999","TOTAL SHARE CAPITAL",AccountClassification.Equity,AccountCategory.SubTotal,AccountNature.Balance,2219999,2219999,"2210000:2219999"),
                T(m,"2200000","2299999","TOTAL EQUITY",AccountClassification.Equity,AccountCategory.CategoryTotal,AccountNature.Balance,2299999,2299999,"2200001:2299999"),
                T(m,"2000000","2999999","TOTAL EQUITY AND LIABILITIES",AccountClassification.Equity,AccountCategory.GrandTotal,AccountNature.Balance,2999999,2999999,"2100001:2999999"),
                T(m,"3111000","3111999","TOTAL INTEREST INCOME",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3111999,3111999,"3111000"),
                T(m,"3112000","3112999","TOTAL LOAN FEE INCOME",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3112999,3112999,"3112000"),
                T(m,"3113000","3113999","TOTAL COMMISSION INCOME",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3113999,3113999,"3113000"),
                T(m,"3110000","3119999","TOTAL REVENUE FROM LOANS",AccountClassification.Revenue,AccountCategory.SubTotal,AccountNature.Income,3119999,3119999,"3111000:3119999"),
                T(m,"3121000","3121999","TOTAL SAVINGS FEE INCOME",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3121999,3121999,"3121000"),
                T(m,"3120000","3129999","TOTAL SAVINGS INCOME",AccountClassification.Revenue,AccountCategory.SubTotal,AccountNature.Income,3129999,3129999,"3120000:3129999"),
                T(m,"3131000","3131999","TOTAL MOBILE MONEY COMMISSION",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3131999,3131999,"3131000"),
                T(m,"3132000","3132999","TOTAL AGENCY BANKING COMMISSION",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3132999,3132999,"3132000"),
                T(m,"3130000","3139999","TOTAL MOBILE MONEY INCOME", AccountClassification.Revenue,AccountCategory.SubTotal,AccountNature.Income,3139999,3139999,"3130000:3139999"),
                T(m,"3141000","3141999","TOTAL BANK INTEREST",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3141999,3141999,"3141000"),
                T(m,"3142000","3142999","TOTAL MISCELLANEOUS INCOME",AccountClassification.Revenue,AccountCategory.HeaderTotal,AccountNature.Income,3142999,3142999,"3142000"),
                T(m,"3140000","3149999","TOTAL OTHER INCOME",AccountClassification.Revenue,AccountCategory.SubTotal,AccountNature.Income,3149999,3149999,"3141000:3149999"),
                T(m,"3100000","3199999","TOTAL REVENUES",AccountClassification.Revenue,AccountCategory.CategoryTotal,AccountNature.Income,3199999,3199999,"3100001:3199999"),
                T(m,"4110000","4119999","TOTAL MEETING EXPENSES",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4119999,4119999,"4110000"),
                T(m,"4100000","4199999","TOTAL ADMIN EXPENSES",AccountClassification.Expense,AccountCategory.SubTotal,AccountNature.Income,4199999,4199999,"4100000:4199999"),
                T(m,"4210000","4219999","TOTAL MARKETING",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4219999,4219999,"4210000"),
                T(m,"4220000","4229999","TOTAL ADVERTISING",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4229999,4229999,"4220000"),
                T(m,"4200000","4299999","TOTAL MARKETING EXPENSES",AccountClassification.Expense,AccountCategory.SubTotal,AccountNature.Income,4299999,4299999,"4210000:4299999"),
                T(m,"4310000","4319999","TOTAL OFFICE EXPENSES",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4319999,4319999,"4310000"),
                T(m,"4320000","4329999","TOTAL OTHER OPERATIONS",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4329999,4329999,"4320000"),
                T(m,"4300000","4399999","TOTAL OPERATION EXPENSES",AccountClassification.Expense,AccountCategory.SubTotal,AccountNature.Income,4399999,4399999,"4300000:4399999"),
                T(m,"4410000","4419999","TOTAL BANK CHARGES",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4419999,4419999,"4410000"),
                T(m,"4420000","4429999","TOTAL LICENSES",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4429999,4429999,"4420000"),
                T(m,"4400000","4499999","TOTAL FINANCIAL EXPENSES",AccountClassification.Expense,AccountCategory.SubTotal,AccountNature.Income,4499999,4499999,"4410000:4499999"),
                T(m,"4510000","4519999","TOTAL SALARIES",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4519999,4519999,"4510000"),
                T(m,"4520000","4529999","TOTAL EMPLOYEE BENEFITS",AccountClassification.Expense,AccountCategory.HeaderTotal,AccountNature.Income,4529999,4529999,"4520000"),
                T(m,"4500000","4599999","TOTAL PAYROLL EXPENSES", AccountClassification.Expense,AccountCategory.SubTotal,AccountNature.Income,4599999,4599999,"4500000:4599999"),
                T(m,"4000000","4999999","TOTAL EXPENSES",AccountClassification.Expense,AccountCategory.CategoryTotal, AccountNature.Income,4999999,4999999,"4100000:4999999"),
            ];
        }

        #endregion

        #region Journal Types

        private static readonly (string Code, string Name, string Series)[] SystemJournalTypes =
        [
            ("JNLGNL","General Journal", "JNL100"),
            ("JNLREC","Receipts Journal", "JNL200"),
            ("JNLPAY","Payments Journal","JNL300"),
            ("JNLSAL","Sales Journal", "JNL400"),
            ("JNLIBJ","Inter-branch Journal","JNL500"),
            ("JNLFEX","Foreign Currency Journal", "JNL600"),
        ];

        private static async Task SeedJournalTypesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.JournalTypes
                .Select(j => j.SeriesIdentifier)
                .ToHashSetAsync();

            var toInsert = SystemJournalTypes
                .Where(j => !existing.Contains(j.Code))
                .Select(j => new JournalType
                {
                    SeriesIdentifier = j.Code,
                    JournalName = j.Name,
                    AccountClassification = AccountClassification.Open,
                    AllowTaxDifference = j.Code == "JNLGNL",
                    RequireVoucher = false,
                    MultiCurrency = false,
                    IsSystem = true,
                    IsActive = j.Code == "JNLGNL",  // only General Journal active by default
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.JournalTypes.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} journal type(s).", "SEED");
            }
            else
                logger.Log("Journal types already seeded.", "SEED");
        }

        #endregion

        #region Voucher Types

        private static readonly (string Code, string Name, string Series)[] SystemVoucherTypes =
        [
            ("VOUGNV", "General Voucher", "VOU100"),
            ("VOUPRV", "Prepaid Voucher","VOU200"),
            ("VOURCV", "Recurring Voucher", "VOU300"),
            ("VOUTRV", "Transfer Voucher","VOU400"),
            ("VOUADV", "Adjustment Voucher", "VOU500"),
            ("VOUPSV", "Provision Voucher", "VOU600"),
        ];

        private static async Task SeedVoucherTypesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.VoucherTypes
                .Select(v => v.SeriesIdentifier)
                .ToHashSetAsync();

            var toInsert = SystemVoucherTypes
                .Where(v => !existing.Contains(v.Code))
                .Select(v => new VoucherType
                {
                    SeriesIdentifier = v.Code,
                    VoucherName = v.Name,
                    Posting = PostingType.Both,
                    Category = VoucherCategory.Journal,
                    IsSystem = true,
                    IsActive = v.Code == "VOUGNV",  
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.VoucherTypes.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} voucher type(s).", "SEED");
            }
            else
                logger.Log("Voucher types already seeded.", "SEED");
        }

        #endregion

        #region Transaction Document Types

        private static readonly (string Code, string Name)[] DocumentTypes =
        [
            ("RCP","Receipt"),
            ("PV", "Payment Voucher"),
            ("JV", "Journal Voucher"),
            ("BNK","Bank Statement Entry"),
            ("INV","Invoice"),
            ("CN","Credit Note"),
            ("DN","Debit Note"),
            ("DSB","Disbursement Slip"),
            ("TFR","Transfer Document"),
            ("SHR", "Share Certificate"),
        ];

        private static async Task SeedDocumentTypesAsync(AppDataContext context, IServiceLogger logger) {
            var existing = await context.TransactionDocumentTypes
                .Select(t => t.Code)
                .ToHashSetAsync();

            var toInsert = DocumentTypes
                .Where(d => !existing.Contains(d.Code))
                .Select(d => new TransactionDocumentType
                {
                    Code = d.Code,
                    TypeName = d.Name,
                    IsSystem = true,
                    CreatedBy = "SYSTEM"
                }).ToList();

            if (toInsert.Count > 0) {
                await context.TransactionDocumentTypes.AddRangeAsync(toInsert);
                await context.SaveChangesAsync();
                logger.Log($"Seeded {toInsert.Count} document type(s).", "SEED");
            }
            else
                logger.Log("Document types already seeded.", "SEED");
        }

        #endregion
    }

}
