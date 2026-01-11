CREATE TABLE Clientes (
    CodigoCliente INT IDENTITY(1,1) PRIMARY KEY,
    RazaoSocial NVARCHAR(200) NOT NULL,
    NomeFantasia NVARCHAR(200),
    TipoPessoa NVARCHAR(20),
    Documento NVARCHAR(50),
    Endereco NVARCHAR(300),
    Complemento NVARCHAR(100),
    Bairro NVARCHAR(100),
    Cidade NVARCHAR(100),
    CEP NVARCHAR(20),
    UF NVARCHAR(2),
    DataInsercao DATETIME2 NOT NULL,
    UsuarioInsercao NVARCHAR(100)
);

CREATE TABLE TiposTelefone (
    CodigoTipoTelefone INT IDENTITY(1,1) PRIMARY KEY,
    DescricaoTipoTelefone NVARCHAR(100) NOT NULL,
    DataInsercao DATETIME2 NOT NULL,
    UsuarioInsercao NVARCHAR(100)
);


CREATE TABLE Telefones (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CodigoCliente INT NOT NULL,
    NumeroTelefone NVARCHAR(30) NOT NULL,
    CodigoTipoTelefone INT NOT NULL,
    Operadora NVARCHAR(50),
    Ativo BIT NOT NULL,
    DataInsercao DATETIME2 NOT NULL,
    UsuarioInsercao NVARCHAR(100),

    CONSTRAINT FK_Telefones_Clientes
        FOREIGN KEY (CodigoCliente) REFERENCES Clientes(CodigoCliente),

    CONSTRAINT FK_Telefones_TiposTelefone
        FOREIGN KEY (CodigoTipoTelefone) REFERENCES TiposTelefone(CodigoTipoTelefone)
);
