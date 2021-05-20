# payment

.Net EF Core Web Api

StartUp Project : Payment.Api

DB : SQLite in Memory is used. In appsettings.json, The file-based connection string is also left as a comment for file-based use of SQlite in appsettings.json.

Card Fee : Card Fee is applied for Message-Type Payment only.

ADJUSTMENT : Each Transaction is inserted as a new Record to AccountTransaction Table. ADJUSTMENT is applied, If There is a previous transaction with the same transactionId.
