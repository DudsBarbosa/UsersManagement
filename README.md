# Gest�o de Usu�rios
## Objetivo
O objetivo deste projeto � criar um sistema de gest�o de usu�rios, onde � poss�vel cadastrar, editar, excluir e listar usu�rios.

## Executando o projeto localmente
1. Criar o banco de dados no SQL Server. Os scripts est�o no projeto 'Infrastructure', pasta 'DatabaseScripts'.
1. Para executar o projeto, voc� deve ter o Visual Studio 2022 e o SQL Server instalados em seu computador.
1. A string de conex�o est� no arquivo `appsettings.json` do projeto `Web`.
1. O projeto est� dispon�vel em `https://github.com/DudsBarbosa/UsersManagement` e tamb�m em um arquivo compactado enviado por e-mail.

As funcionalidade funcionam apenas com o aplicativo Web em execu��o. A partir da pasta Web, deve executar dotnet run --launch-profile Web. Agora voc� deve poder navegar para https://localhost:[port].


## Tecnologias
- .NET Core 8
- SQL Server
- Entity Framework Core
- JavaScript
- jQuery

## Arquitetura
As aplica��es que seguem o Princ�pio da Invers�o de Depend�ncias, bem como os princ�pios do Domain-Driven Design (DDD), tendem a chegar a uma arquitetura semelhante. Esta arquitetura tem tido muitos nomes ao longo dos anos. Um dos primeiros nomes foi Arquitetura Hexagonal, seguido de Ports-and-Adapters. Mais recentemente, foi citada como a Arquitetura Onion ou Clean Architecture.

Esta aplica��o busca utilizar a abordagem Clean Architecture para organizar o c�digo em projetos.

Clean Architecture coloca a l�gica de neg�cios e o modelo de aplicativo no centro do aplicativo. Em vez de a l�gica de neg�cios depender do acesso a dados ou de outras preocupa��es de infraestrutura, essa depend�ncia � invertida: os detalhes de infraestrutura e implementa��o dependem do Application Core. Esta funcionalidade � conseguida atrav�s da defini��o de abstra��es, ou interfaces, no n�cleo da aplica��o, que s�o depois implementadas por tipos definidos na camada de infraestrutura. Uma forma comum de visualizar esta arquitetura � utilizar uma s�rie de c�rculos conc�ntricos, semelhante a uma cebola. A Figura 5-7 mostra um exemplo deste estilo de representa��o arquitet�nica.

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-7.png

Figura 1-1. Arquitetura limpa; visualiza��o em cebola

Nesse diagrama, as depend�ncias fluem em dire��o ao c�rculo mais interno. O Application Core (n�cleo do aplicativo) tem esse nome devido � sua posi��o no centro do diagrama. E voc� pode ver no diagrama que o Application Core n�o tem depend�ncias de outras camadas de aplicativos. As entidades e interfaces do aplicativo est�o bem no centro. Do lado de fora, mas ainda no Application Core, est�o os servi�os de dom�nio, que normalmente implementam as interfaces definidas no c�rculo interno. Fora do n�cleo do aplicativo, tanto a interface do usu�rio quanto as camadas de infraestrutura dependem do n�cleo do aplicativo, mas n�o umas das outras (necessariamente).

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-8.png

A Figura 1-2 mostra um diagrama de camadas horizontais mais tradicional que reflete melhor a depend�ncia entre a interface do usu�rio e outras camadas.

Observe que as setas s�lidas representam depend�ncias de tempo de compila��o, enquanto a seta tracejada representa uma depend�ncia somente de tempo de execu��o. Com a arquitetura limpa, a camada de interface do usu�rio trabalha com interfaces definidas no Application Core em tempo de compila��o e, idealmente, n�o deveria saber sobre os tipos de implementa��o definidos na camada de infraestrutura. No entanto, em tempo de execu��o, esses tipos de implementa��o s�o necess�rios para que o aplicativo seja executado, portanto, precisam estar presentes e conectados �s interfaces do Application Core por meio de inje��o de depend�ncia.

https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/media/image5-9.png
Figura 1-3. Diagrama de arquitetura do ASP.NET Core seguindo a Clean Architecture.

Em uma solu��o de arquitetura limpa, cada projeto tem responsabilidades claras. Dessa forma, certos tipos pertencem a cada projeto e voc� frequentemente encontrar� pastas correspondentes a esses tipos no projeto apropriado.

#### Application Core

O n�cleo do aplicativo cont�m o modelo de neg�cios, que inclui entidades, servi�os e interfaces. Essas interfaces incluem abstra��es para opera��es que ser�o executadas usando a infraestrutura, como acesso a dados, acesso ao sistema de arquivos, chamadas de rede etc. �s vezes, os servi�os ou interfaces definidos nessa camada precisar�o trabalhar com tipos que n�o s�o entidades e que n�o dependem da interface do usu�rio ou da infraestrutura. Eles podem ser definidos como DTOs (Data Transfer Objects, objetos de transfer�ncia de dados) simples.

Tipos de n�cleo do aplicativo:
1. Entidades (classes de modelo de neg�cios que s�o persistidas)
1. Agregados (grupos de entidades)
1. Interfaces
1. Servi�os de dom�nio
1. Especifica��es
1. Exce��es personalizadas e cl�usulas de prote��o
1. Eventos e manipuladores de dom�nio

#### Infraestrutura
O projeto de infraestrutura normalmente inclui implementa��es de acesso a dados. Em um aplicativo da Web ASP.NET Core t�pico, essas implementa��es incluem o DbContext do Entity Framework (EF), quaisquer objetos de migra��o do EF Core que tenham sido definidos e classes de implementa��o de acesso a dados. A maneira mais comum de abstrair o c�digo de implementa��o de acesso a dados � por meio do uso do padr�o de design Repository.

Al�m das implementa��es de acesso a dados, o projeto Infrastructure deve conter implementa��es de servi�os que precisam interagir com as preocupa��es da infraestrutura. Esses servi�os devem implementar interfaces definidas no Application Core e, portanto, o projeto Infrastructure deve ter uma refer�ncia ao projeto Application Core.

Tipos de infraestrutura:
1. Tipos do n�cleo do EF (DbContext, Migra��o)
1. Tipos de implementa��o de acesso a dados (Reposit�rios)
1. Servi�os espec�ficos de infraestrutura (por exemplo, log de arquivos)

#### Camada da interface do usu�rio
A camada de interface do usu�rio em um aplicativo ASP.NET Core MVC � o ponto de entrada do aplicativo. Esse projeto deve fazer refer�ncia ao projeto Application Core, e seus tipos devem interagir com a infraestrutura estritamente por meio de interfaces definidas no Application Core. Nenhuma instancia��o direta ou chamada est�tica para os tipos da camada de infraestrutura deve ser permitida na camada da interface do usu�rio.

Tipos da camada de interface do usu�rio:
1. Controladores
1. Filtros personalizados
1. Middleware personalizado
1. Visualiza��es
1. ViewModels
1. Inicializa��o

O arquivo Program.cs � respons�vel pela configura��o do aplicativo e pela conex�o dos tipos de implementa��o �s interfaces. O local em que essa l�gica � executada � conhecido como a raiz de composi��o do aplicativo e � o que permite que a inje��o de depend�ncia funcione corretamente em tempo de execu��o.