# LegalScraper
Repositório destinado à prova de conceito para projetos de web crawler e scraper.

<h1 align="center">Welcome to Legal Scraper 👋</h1>
<p>
  <img alt="Version" src="https://img.shields.io/badge/version- 1.1.1-blue.svg?cacheSeconds=2592000" />
  <a href="https://twitter.com/MarcoManhaes" target="_blank">
    <img alt="Twitter: MarcoManhaes" src="https://img.shields.io/twitter/follow/MarcoManhaes.svg?style=social" />
  </a>
</p>

> Repositório destinado à prova de conceito para projetos de web crawler e scraper.

## Status

Em Desenvolvimento ⚠️


## Execução

```sh
Baixar o projeto em uma estação local.

Utilizando o Visual studio:
Abra a Solution contendo o conjunto de projetos (camadas) que integram o projeto como um todo Legal Scraper, 
compile a solution e rode o projeto com F5 ou Ctrl +F5 (Debug)

Utilizando linha de comando:
No diretório raiz do projeto (onde está localizada a solution)execute o comando dotnet build.
Após a compilação sem erros, entre no riretório do projeto LegalScraper.Client execute o comando dotnet run.
```

## Como usar
> ### Pre Requisitos
>   É necessário que a estação que executará o projeto contenha o Microsoft .NET Core SDK 3.1 instalado.

>   Caso deseja rodar o projeto em modo debug e analizar o código fonte com maiores detalhes, 
>   é necessário ter o Visual studio (preferencialmente 2019) ou VS Code instalados.

> ### Local de arquivos
> Existem dois arquivos importantes gerados durante a execução do projetosão eles: [nome_base_dados].sqlite e [nome_log].txt

> Base de dados:  a base de dados é gerada com o onome legal_scraper_db.sqlite no diretório:
Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LegalScraper.Application");

> Arq uivo de log: o arquivo de log é gerado com o nome no formato {0:yyyyMMdd}_{1}.log (Ex: 20220713_LegalScraper.log) no diretório:
Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Logs");

## Tecnologias utilizadas

```sh
* Microsoft.NetCore
* Console application Core
* Asp.net Core
* Dependency Injection
* Entity Framework Core
* Fluent Migration
* Swashbuckle Swagger
* Newtonsoft.json
* Sqlite
* Selenium
* Unity
* NLog
```

## Author

👤 **Marco Manhães Júnior**

* Twitter: [@MarcoManhaes](https://twitter.com/MarcoManhaes)
* Github: [@MarcoManhaes](https://github.com/MarcoManhaes)
* LinkedIn: [@marco-manhaes](https://linkedin.com/in/marco-manhaes)

## Show your support

Give a ⭐️ if this project helped you!

***
_This README was generated with ❤️ by [readme-md-generator](https://github.com/kefranabg/readme-md-generator)_
