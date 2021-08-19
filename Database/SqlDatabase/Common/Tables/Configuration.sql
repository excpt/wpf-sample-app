CREATE TABLE [Common].[Configuration] (
    [Key]         NVARCHAR (250) NOT NULL,
    [Value]       NVARCHAR (MAX) NOT NULL,
    [IsEncrypted] BIT            NOT NULL,
    [NewField]    NVARCHAR (1000) NOT NULL,
    [test] TIMESTAMP NOT NULL, 
    PRIMARY KEY CLUSTERED ([Key] ASC)
);

