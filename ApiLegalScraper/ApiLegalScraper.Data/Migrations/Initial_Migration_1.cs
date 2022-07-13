using FluentMigrator;

namespace LegalScraper.Data.Migrations
{

    [Migration(1)]
    public class Initial_Migration_1 : Migration
    {
        public override void Up()
        {
            #region[+] Processo
            string sqlProcesso = @"CREATE TABLE Processo (
                                    Id           INTEGER       NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                    Numero       VARCHAR (25)  UNIQUE NOT NULL,
                                    Classe       VARCHAR (25),
                                    Area         VARCHAR (25),
                                    Origem       VARCHAR (150),
                                    Distribuicao VARCHAR (100),
                                    Relator      VARCHAR (100) );";
            #endregion

            #region[+] Movimentacao
            string sqlMovimentacao = @"CREATE TABLE Movimentacao (
                                        Id         INTEGER       NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                        ProcessoId INTEGER       REFERENCES Processo (Id),
                                        Descricao  VARCHAR (250),
                                        Data       DATETIME      NOT NULL
                                    );";
            #endregion

            #region[+] Execute
            Execute.Sql(sqlProcesso);
            Execute.Sql(sqlMovimentacao);
            #endregion
        }

        public override void Down()
        {
            #region[+] Downgrade
            Delete.Table("Processo");
            Delete.Table("Movimentacao");
            #endregion
        }
    }
}
