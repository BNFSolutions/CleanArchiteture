-- Aumenta o tamanho das colunas de Codigo e Descricao SEM apagar dados.
-- Codigo:    nchar(10)  -> nvarchar(30)
-- Descricao: nvarchar(50) -> nvarchar(100)
-- Seguro de rodar mais de uma vez.

USE CleanArchiteture;
GO

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'CATEGORIA' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    IF COL_LENGTH('dbo.CATEGORIA', 'COD_CATEGORIA') IS NOT NULL
        AND COL_LENGTH('dbo.CATEGORIA', 'COD_CATEGORIA') < 60
    BEGIN
        ALTER TABLE dbo.CATEGORIA ALTER COLUMN COD_CATEGORIA nvarchar(30) NOT NULL;
    END

    IF COL_LENGTH('dbo.CATEGORIA', 'DESC_CATEGORIA') IS NOT NULL
        AND COL_LENGTH('dbo.CATEGORIA', 'DESC_CATEGORIA') < 200
    BEGIN
        ALTER TABLE dbo.CATEGORIA ALTER COLUMN DESC_CATEGORIA nvarchar(100) NOT NULL;
    END
END
GO

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PRODUTOS' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    IF COL_LENGTH('dbo.PRODUTOS', 'COD_PRODUTO') IS NOT NULL
        AND COL_LENGTH('dbo.PRODUTOS', 'COD_PRODUTO') < 60
    BEGIN
        ALTER TABLE dbo.PRODUTOS ALTER COLUMN COD_PRODUTO nvarchar(30) NOT NULL;
    END

    IF COL_LENGTH('dbo.PRODUTOS', 'DESC_PRODUTO') IS NOT NULL
        AND COL_LENGTH('dbo.PRODUTOS', 'DESC_PRODUTO') < 200
    BEGIN
        ALTER TABLE dbo.PRODUTOS ALTER COLUMN DESC_PRODUTO nvarchar(100) NOT NULL;
    END
END
GO

-- Remove espacos a direita que o nchar(10) antigo deixava nos codigos ja gravados.
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'CATEGORIA' AND schema_id = SCHEMA_ID('dbo'))
    UPDATE dbo.CATEGORIA SET COD_CATEGORIA = RTRIM(COD_CATEGORIA);
GO

IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'PRODUTOS' AND schema_id = SCHEMA_ID('dbo'))
    UPDATE dbo.PRODUTOS SET COD_PRODUTO = RTRIM(COD_PRODUTO);
GO
