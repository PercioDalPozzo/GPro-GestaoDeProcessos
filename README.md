# GPro - Gestão De Processos
[![NPM](https://img.shields.io/npm/l/react)](https://github.com/PercioDalPozzo/GPro-GestaoDeProcessos/blob/master/LICENSE)

# Sobre o projeto
Este projeto refere-se a uma API para gestão de processos judiciais.
É composto por um cadastro de responsáveis e um cadastro de processos e poderá se consumida de várias formas, 
via integração, interfaces web, interfaces mobile, etc.<br><br>
<b>Comportamentos para responsável e processo </b>
<br>
* Pesquisar<br>
* Visualizar<br>
* Inserir<br>
* Editar<br>

# Tecnologias utilizadas
Visual Studio 2019 com ASP.NET Core (Sdk .NET Core 3.1)<br>
Arquitetura MVC<br>
<code>https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&rel=16</code>
<br>
<br>
Banco de dados PostgreSQL versão 12.7<br>
Instalação não tem segredo. Next, next, finish!<br>
<code>https://www.enterprisedb.com/postgresql-tutorial-resources-training?cid=48</code>
<br>
<br>
DBeaver para manipulação do banco<br>
Ferramenta opcional, se preferir pode acessar o banco pelo pgAdmin4 que é instalado juntamente com o PostgreSQL.<br>
Essa ferramenta é bem tranquila, dispensa maiores explicações.<br>
<code>https://dbeaver.io/files/dbeaver-ce-latest-x86_64-setup.exe</code><br>
<br>
<br>
## Dependências (NuGet)
Provider para conexão com banco PG<br>
Npgsql.EntityFrameworkCore.PostgreSQL<br>
Versão 5.0.6<br>
<br>
Moq<br>
By Daniel Cazzulino, kzu<br>
Versão 4.16.1<br>
<br>
Swagger <br>
Swashbuckle.AspNetCore 5.5.1<br>
<br>
Serilog <br>
Serilog.AspNetCore<br>
Versão 4.1.0<br>
<br>
Para o LazyLoad - Instalar pode linha de comando pode dentro do VS<br>
install-package Microsoft.EntityFrameworkCore.Proxies<br>

# Baixar o projeto
<code>https://github.com/PercioDalPozzo/GPro-GestaoDeProcessos/archive/refs/heads/master.zip</code>

# Criar o banco de dados
Na ferramenta de banco, configurar uma nova conexão e rodar o script para criação do banco.<br>
Para esse projeto, criei a estrutura no próprio Database postgres, mas é recomendável criar um database próprio para o projeto. Fica a seu critério.<br>
A configuração para conexão com o banco fica no "appsettings" na sessão "ConnectionStrings".
Os scripts estão na pasta SQL do repositório, mas pode ser baixado por aqui<br>
<code>https://github.com/PercioDalPozzo/GPro-GestaoDeProcessos/blob/master/Repositorio/SQL/Script_criacao_banco.sql</code>


# Tudo pronto pro F5? Agora vamos conversar com o frontend!
Banco criado, solução aberta no VS, se tudo estiver ok não teremos problema na execução.<br> 
Ao rodar a aplicação será aberto a pagina com documentação técnica dos endpoints.
<code>https://localhost:44355/swagger/index.html</code>

## Comportamento padrão para os Endpoits da camada view web 
Apesar do swagger montar a documentação completa, vale detalhar mais aqui alguns pontos<br>
Toda, <b>TODA</b> requisição independente da ação terá o mesmo padrão de retorno:
* Sucesso (bool) => retorna se a requisição teve sucesso ou falha
* Msg (string) => uma possível mensagem caso a requisição tenha falha
* Content (object) => O resultado da requisição. Poderá ter ou não uma informação.

## Ações de ambos controllers (Responsável e Processo)
* Pesquisar - Recebe as informações de "Pagina" e "Limite" para fazer a paginação e a limitação de quantos registros por pagina.
O retorno da pesquisa estará dentro do "Content" e possui:<br> 
"TotalPaginas" => Quantidade total de páginas considerando os filtros e o limite de registro por pagina<br>
"TotalRegistros" => Quantidade total de registros considerando os filtros<br>
"Registros" => a listagem paginada com o resultado da pesquisa<br>
* PrepararEdicao - Recebe um "Id" e retorna os campos disponíveis para edição ou visualização.
* Salvar => Recebe uma view e obrigatoriamente precisa ter um "Id".<br>
Caso este "Id" for 0 (Zero) o backend interpretará como uma inserção de um novo registro.<br>
Case o "Id" tenha valor, será feita a edição do registro com os dados da view.<br>
No retorno é exposto o Id caso quem consuma necessite de alguma outra ação com o registro recém inserido.<br>

## Controller de responsável
* PrepararEdicao => Retorna os dados do responsavel juntamente com a lista dos processos vinculados (limitado em 10 registros)<br>
* Pesquisar => Campos disponíveis: Nome, Cpf, NumeroProcesso (limitado aos ultimos 4 processos)<br>
* Salvar => Nome, Email, Cpf, Foto e AtualizarFoto<br>
Obs: O campo bool "AtualizarFoto", que deve ser passado como "True" apenas se tiver alteração da foto.
Isso para evitar trafego desnecessário caso o usuário queira alterar apenas os dados cadastrais.
Caso não tenha alteração da foto, enviar como "False" e NÃO mandar nada o campo "Foto".

## Controller de processo
* Para controle de tela sobre permitir ou não edição, será retornada uma propriedade "Finalizada" baseada na situação do processo.

## Envio de e-mail
Foi criado um e-mail no gmail para fazer o envio. As configurações para envio estão no "appsettings" na sessão "ConfigEnvioEmail".<br>
As configurações são bem fáceis e caso deseje mudar para mandar por outro emitente, precisa lembrar de mudar as configurações da conta
"Acesso a app menos seguro".

# Integrações
Foi criado outros 4 controllers para outras integração com as mesmas funcionalidades descritas até aqui.
Cada um poderá implementar a entrada e saída da informação conforme a particularidade de cada aplicação.

# Teste unitário
Por dentro do visual studio, acessar no meu "Teste" >> "Executar todos os testes".


# Teste de carga
Não tenho base para saber na pratica qual o volume de informação num ambiente real.<br>
Então então populei a base com 100.000 responsáveis e 1.000.000 de processos.<br>
Cada processo foi vinculado a 3 responsáveis.<br>
O script abaixo zera o banco e popula com a carga de dados citada.<br>
<code>https://github.com/PercioDalPozzo/GPro-GestaoDeProcessos/blob/master/Repositorio/SQL/Script_teste_carga.sql</code>

# Log
Utilizado o Serilog. Está configurado para gravar o log das requisições na pasta "Log" dentro da pasta da aplicação.


# Sugestão para melhoria no projeto
* Retornar as validações em uma listagem de críticas invés de uma exceção para cada validação.

* Pesquisa de Responsável
Não colocaria na pesquisa de responsável a responsabilidade de carregar os processos. 
Julgo que o cadastro é apenas para cadastrar e não deve-se misturar processos complexos nesse local.
Para sanar essa necessidade, minha sugestão seria criar uma outra ferramenta para consulta como 
um dashboard ou até mesmo um relatório que cruza essas informações.

# Docker
Não está 100%<br>
<br>
Instalação <br>
<code>https://desktop.docker.com/win/stable/amd64/Docker%20Desktop%20Installer.exe</code>
<br>
<br>
Material para referência<br>
<code>https://docs.microsoft.com/pt-br/dotnet/core/docker/build-container?tabs=windows</code>
<br>
<br>
Consultar a imagem criada<br>
<code>docker images</code>
<br>
<br>
Criar e subir o container<br>
<code>docker run -it --rm -p 5000:443 --name gpro-container gpro-image</code>


# Autor 

Pércio Ebsen Dal Pozzo

pe.bass@gmail.com

https://www.linkedin.com/in/p%C3%A9rcio-ebsen-dal-pozzo-099a3425/


