# Chamados Service

Projeto criado utilizando Clean Archtecture. Fornece o cadastro de chamados para uma empresa fictícia.

>  ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/brunovicenteb/chamados-service/Build-Tests) ![GitHub last commit](https://img.shields.io/github/last-commit/brunovicenteb/chamados-service) ![GitHub top language](https://img.shields.io/github/languages/top/brunovicenteb/chamados-service) ![GitHub language count](https://img.shields.io/github/languages/count/brunovicenteb/chamados-service) ![GitHub repo size](https://img.shields.io/github/repo-size/brunovicenteb/chamados-service) [![License](https://img.shields.io/badge/license-MIT-green)](./LICENSE) 

### Tecnologias utilizadas no desenvolvimento do projeto

<table>
  <tr>
    <th></th>
    <th>Tecnologia</th>
    <th>Versão</th>
    <th>Ferramentas</th>    
  </tr>
  <tr>
    <td><img align="center" alt="Rafa-Csharp" height="30" width="40" src="https://icongr.am/devicon/dot-net-original.svg?size=40"></td>
    <td>.Net Core</td>
    <td>6.0</td>
    <td><a href="https://serilog.net">Serilog</a>, <a href="https://nunit.org">NUnit</a>, <a href="https://fluentvalidation.net">FluentValidation</a></td>
  </tr>
  <tr>
    <td><img align="center" alt="Rafa-Csharp" height="30" width="40" src="https://icongr.am/devicon/csharp-original.svg?size=40"></td>
    <td>C#</td>
    <td>9.0</td>
    <td></td>
  </tr>    
  <tr>
    <td><img align="center" alt="Rafa-Csharp" height="30" width="40" src="https://icongr.am/devicon/visualstudio-plain.svg?size=40"></td>
    <td>Visual Studio</td>
    <td>2022 Community</td>
    <td></td>
  </tr>    
  <tr>
    <td><img align="center" alt="Rafa-Csharp" height="30" width="40" src="https://icongr.am/devicon/mongodb-original.svg?size=40"></td>
    <td>MongoDB</td>
    <td>5.0.6 Community</td>
    <td><a href="https://docs.mongodb.com/compass/current">MongoDB Compass</a>, <a href="https://docs.mongodb.com/drivers/csharp">MongoDB.Driver</a></td>    
  </tr>
  <tr>
    <td><img align="center" alt="Rafa-Csharp" height="30" width="40" src="https://icongr.am/devicon/git-original.svg?size=40"></td>
    <td>Git</td>
    <td>lasted</td>
    <td><a href="https://github.com/nvie/gitflow">Git Flow</a></td>    
  </tr>  
</table>

## Armazenamento dos dados

Conforme informado na tabela acima, o banco de dados utilizado foi o **MongoDB** e está armazenado de forma gratuita no [Atlas](https://www.mongodb.com/cloud/atlas).

## Pré-requisitos para a instalação do projeto:

+ Git
+ Docker
+ Docker-compose

## Instalação do projeto e configuração do ambiente:

1. Clonar o repositório:

   `
   git clone https://github.com/brunovicenteb/chamados-service.git
   `

2. Entrar no diretório criado:

   `
   cd chamados-service
   `

4. Subir o container:

   `
   docker-compose -p challenge up -d
   `
  
Nessa etapa **já é possível acessar** a [API](http://localhost:8000/) e a [documentação](http://localhost:8000/swagger/index.html) no formato Open API 3.0; pelo navegador.

# License

[The MIT License (MIT)](LICENSE)
