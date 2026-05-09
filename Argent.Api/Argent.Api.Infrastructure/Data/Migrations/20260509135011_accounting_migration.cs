using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Argent.Api.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class accounting_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_acc_bank_ledger_acc_bank_accounts_BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_acc_bank_ledger_acc_transaction_documents_TransactionDocum~1",
                schema: "mfi",
                table: "acc_bank_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_acc_card_ledger_acc_cards_CardId1",
                schema: "mfi",
                table: "acc_card_ledger");

            migrationBuilder.DropForeignKey(
                name: "FK_acc_cash_accounts_acc_ledger_accounts_LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_acc_journal_entries_acc_general_ledger_GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_acc_voucher_lines_acc_transaction_documents_TransactionDoc~1",
                schema: "mfi",
                table: "acc_voucher_lines");

            migrationBuilder.DropForeignKey(
                name: "FK_lnr_loan_officer_users_AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer");

            migrationBuilder.DropIndex(
                name: "IX_lnr_loan_officer_AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer");

            migrationBuilder.DropIndex(
                name: "IX_acc_voucher_lines_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_voucher_lines");

            migrationBuilder.DropIndex(
                name: "IX_acc_journal_entries_GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries");

            migrationBuilder.DropIndex(
                name: "IX_acc_cash_accounts_LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts");

            migrationBuilder.DropIndex(
                name: "IX_acc_card_ledger_CardId1",
                schema: "mfi",
                table: "acc_card_ledger");

            migrationBuilder.DropIndex(
                name: "IX_acc_bank_ledger_BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger");

            migrationBuilder.DropIndex(
                name: "IX_acc_bank_ledger_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_bank_ledger");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer");

            migrationBuilder.DropColumn(
                name: "TransactionDocumentId1",
                schema: "mfi",
                table: "acc_voucher_lines");

            migrationBuilder.DropColumn(
                name: "GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries");

            migrationBuilder.DropColumn(
                name: "LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts");

            migrationBuilder.DropColumn(
                name: "CardId1",
                schema: "mfi",
                table: "acc_card_ledger");

            migrationBuilder.DropColumn(
                name: "BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger");

            migrationBuilder.DropColumn(
                name: "TransactionDocumentId1",
                schema: "mfi",
                table: "acc_bank_ledger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionDocumentId1",
                schema: "mfi",
                table: "acc_voucher_lines",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CardId1",
                schema: "mfi",
                table: "acc_card_ledger",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TransactionDocumentId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lnr_loan_officer_AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_voucher_lines_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionDocumentId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_journal_entries_GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "GeneralLedgerEntryId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_cash_accounts_LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts",
                column: "LedgerAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_card_ledger_CardId1",
                schema: "mfi",
                table: "acc_card_ledger",
                column: "CardId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_ledger_BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "BankAccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_acc_bank_ledger_TransactionDocumentId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "TransactionDocumentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_bank_ledger_acc_bank_accounts_BankAccountId1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "BankAccountId1",
                principalSchema: "mfi",
                principalTable: "acc_bank_accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_bank_ledger_acc_transaction_documents_TransactionDocum~1",
                schema: "mfi",
                table: "acc_bank_ledger",
                column: "TransactionDocumentId1",
                principalSchema: "mfi",
                principalTable: "acc_transaction_documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_card_ledger_acc_cards_CardId1",
                schema: "mfi",
                table: "acc_card_ledger",
                column: "CardId1",
                principalSchema: "mfi",
                principalTable: "acc_cards",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_cash_accounts_acc_ledger_accounts_LedgerAccountId1",
                schema: "mfi",
                table: "acc_cash_accounts",
                column: "LedgerAccountId1",
                principalSchema: "mfi",
                principalTable: "acc_ledger_accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_journal_entries_acc_general_ledger_GeneralLedgerEntryId1",
                schema: "mfi",
                table: "acc_journal_entries",
                column: "GeneralLedgerEntryId1",
                principalSchema: "mfi",
                principalTable: "acc_general_ledger",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_acc_voucher_lines_acc_transaction_documents_TransactionDoc~1",
                schema: "mfi",
                table: "acc_voucher_lines",
                column: "TransactionDocumentId1",
                principalSchema: "mfi",
                principalTable: "acc_transaction_documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lnr_loan_officer_users_AppUserId1",
                schema: "mfi",
                table: "lnr_loan_officer",
                column: "AppUserId1",
                principalSchema: "mfi",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
