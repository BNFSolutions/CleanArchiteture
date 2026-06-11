-- Adiciona a coluna CPF na tabela CLIENTE (idempotente).
-- Armazena apenas os 11 digitos (sem pontuacao).

IF COL_LENGTH('CLIENTE', 'CPF') IS NULL
BEGIN
    ALTER TABLE CLIENTE
        ADD CPF NVARCHAR(11) NULL;
END
GO

UPDATE CLIENTE
SET CPF = ''
WHERE CPF IS NULL;
GO

IF EXISTS (
    SELECT 1
    FROM sys.columns
    WHERE object_id = OBJECT_ID('CLIENTE')
      AND name = 'CPF'
      AND is_nullable = 1
)
BEGIN
    ALTER TABLE CLIENTE
        ALTER COLUMN CPF NVARCHAR(11) NOT NULL;
END
GO
