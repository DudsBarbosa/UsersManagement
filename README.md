# Gestão de Usuários
## Objetivo
O objetivo deste projeto é criar um sistema de gestão de usuários, onde é possível cadastrar, editar, excluir e listar usuários.

## Executando o projeto localmente
1. Criar o banco de dados no SQL Server. Os scripts estão no projeto 'Infrastructure', pasta 'DatabaseScripts'.
1. Para executar o projeto, você deve ter o Visual Studio 2022 e o SQL Server instalados em seu computador.
1. A string de conexão está no arquivo `appsettings.json` do projeto `Web`.
1. O projeto está disponível em `https://github.com/DudsBarbosa/UsersManagement` e também em um arquivo compactado enviado por e-mail.

As funcionalidade funcionam apenas com o aplicativo Web em execução. A partir da pasta Web, deve executar dotnet run --launch-profile Web. Agora você deve poder navegar para https://localhost:[port].


## Tecnologias
- .NET Core 8
- SQL Server
- Entity Framework Core
- JavaScript
- jQuery

## Arquitetura
As aplicações que seguem o Princípio da Inversão de Dependências, bem como os princípios do Domain-Driven Design (DDD), tendem a chegar a uma arquitetura semelhante. Esta arquitetura tem tido muitos nomes ao longo dos anos. Um dos primeiros nomes foi Arquitetura Hexagonal, seguido de Ports-and-Adapters. Mais recentemente, foi citada como a Arquitetura Onion ou Clean Architecture.

Esta aplicação busca utilizar a abordagem Clean Architecture para organizar o código em projetos.

Clean Architecture coloca a lógica de negócios e o modelo de aplicativo no centro do aplicativo. Em vez de a lógica de negócios depender do acesso a dados ou de outras preocupações de infraestrutura, essa dependência é invertida: os detalhes de infraestrutura e implementação dependem do Application Core. Esta funcionalidade é conseguida através da definição de abstrações, ou interfaces, no núcleo da aplicação, que são depois implementadas por tipos definidos na camada de infraestrutura. Uma forma comum de visualizar esta arquitetura é utilizar uma série de círculos concêntricos, semelhante a uma cebola. A Figura 5-7 mostra um exemplo deste estilo de representação arquitetônica.

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-7.png

Figura 1-1. Arquitetura limpa; visualização em cebola

Nesse diagrama, as dependências fluem em direção ao círculo mais interno. O Application Core (núcleo do aplicativo) tem esse nome devido à sua posição no centro do diagrama. E você pode ver no diagrama que o Application Core não tem dependências de outras camadas de aplicativos. As entidades e interfaces do aplicativo estão bem no centro. Do lado de fora, mas ainda no Application Core, estão os serviços de domínio, que normalmente implementam as interfaces definidas no círculo interno. Fora do núcleo do aplicativo, tanto a interface do usuário quanto as camadas de infraestrutura dependem do núcleo do aplicativo, mas não umas das outras (necessariamente).

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-8.png

A Figura 1-2 mostra um diagrama de camadas horizontais mais tradicional que reflete melhor a dependência entre a interface do usuário e outras camadas.

Observe que as setas sólidas representam dependências de tempo de compilação, enquanto a seta tracejada representa uma dependência somente de tempo de execução. Com a arquitetura limpa, a camada de interface do usuário trabalha com interfaces definidas no Application Core em tempo de compilação e, idealmente, não deveria saber sobre os tipos de implementação definidos na camada de infraestrutura. No entanto, em tempo de execução, esses tipos de implementação são necessários para que o aplicativo seja executado, portanto, precisam estar presentes e conectados às interfaces do Application Core por meio de injeção de dependência.

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-9.png
Figura 1-3. Diagrama de arquitetura do ASP.NET Core seguindo a Clean Architecture.

Em uma solução de arquitetura limpa, cada projeto tem responsabilidades claras. Dessa forma, certos tipos pertencem a cada projeto e você frequentemente encontrará pastas correspondentes a esses tipos no projeto apropriado.

#### Application Core

O núcleo do aplicativo contém o modelo de negócios, que inclui entidades, serviços e interfaces. Essas interfaces incluem abstrações para operações que serão executadas usando a infraestrutura, como acesso a dados, acesso ao sistema de arquivos, chamadas de rede etc. Às vezes, os serviços ou interfaces definidos nessa camada precisarão trabalhar com tipos que não são entidades e que não dependem da interface do usuário ou da infraestrutura. Eles podem ser definidos como DTOs (Data Transfer Objects, objetos de transferência de dados) simples.

Tipos de núcleo do aplicativo:
1. Entidades (classes de modelo de negócios que são persistidas)
1. Agregados (grupos de entidades)
1. Interfaces
1. Serviços de domínio
1. Especificações
1. Exceções personalizadas e cláusulas de proteção
1. Eventos e manipuladores de domínio

#### Infraestrutura
O projeto de infraestrutura normalmente inclui implementações de acesso a dados. Em um aplicativo da Web ASP.NET Core típico, essas implementações incluem o DbContext do Entity Framework (EF), quaisquer objetos de migração do EF Core que tenham sido definidos e classes de implementação de acesso a dados. A maneira mais comum de abstrair o código de implementação de acesso a dados é por meio do uso do padrão de design Repository.

Além das implementações de acesso a dados, o projeto Infrastructure deve conter implementações de serviços que precisam interagir com as preocupações da infraestrutura. Esses serviços devem implementar interfaces definidas no Application Core e, portanto, o projeto Infrastructure deve ter uma referência ao projeto Application Core.

Tipos de infraestrutura:
1. Tipos do núcleo do EF (DbContext, Migração)
1. Tipos de implementação de acesso a dados (Repositórios)
1. Serviços específicos de infraestrutura (por exemplo, log de arquivos)

#### Camada da interface do usuário
A camada de interface do usuário em um aplicativo ASP.NET Core MVC é o ponto de entrada do aplicativo. Esse projeto deve fazer referência ao projeto Application Core, e seus tipos devem interagir com a infraestrutura estritamente por meio de interfaces definidas no Application Core. Nenhuma instanciação direta ou chamada estática para os tipos da camada de infraestrutura deve ser permitida na camada da interface do usuário.

Tipos da camada de interface do usuário:
1. Controladores
1. Filtros personalizados
1. Middleware personalizado
1. Visualizações
1. ViewModels
1. Inicialização

O arquivo Program.cs é responsável pela configuração do aplicativo e pela conexão dos tipos de implementação às interfaces. O local em que essa lógica é executada é conhecido como a raiz de composição do aplicativo e é o que permite que a injeção de dependência funcione corretamente em tempo de execução.