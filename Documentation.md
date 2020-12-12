# API de Conta Bancária

## Índice

- [API de Conta Bancária](#api-de-conta-bancária)
  - [Índice](#índice)
  - [Sobre a API](#sobre-a-api)
    - [Feito Com](#feito-com)
  - [Endpoints](#endpoints)
    - [Conta Bancária](#conta-bancária)
      - [Criar Conta](#criar-conta)
        - [Exemplo de Body:](#exemplo-de-body)
        - [Possíveis Retornos:](#possíveis-retornos)
      - [Ler Todas As Contas](#ler-todas-as-contas)
        - [Possíveis Retornos:](#possíveis-retornos-1)
      - [Ler Uma Conta](#ler-uma-conta)
        - [Possíveis Retornos:](#possíveis-retornos-2)
      - [Atualizar Uma Conta](#atualizar-uma-conta)
        - [Possíveis Retornos:](#possíveis-retornos-3)
      - [Deletar Uma Conta](#deletar-uma-conta)
        - [Possíveis Retornos:](#possíveis-retornos-4)
    - [Deposito](#deposito)
      - [Exemplo de Request:](#exemplo-de-request)
      - [Possíveis Retornos:](#possíveis-retornos-5)
      - [O que acontece no Depósito:](#o-que-acontece-no-depósito)
      - [Exemplo de Depósito:](#exemplo-de-depósito)
    - [Saque](#saque)
      - [Exemplo de Request:](#exemplo-de-request-1)
      - [Possíveis Retornos:](#possíveis-retornos-6)
      - [O que acontece na Tentiva de Saque:](#o-que-acontece-na-tentiva-de-saque)
      - [Exemplo De Saque Com Sucesso:](#exemplo-de-saque-com-sucesso)
      - [Exemplo De Saque Com Falha:](#exemplo-de-saque-com-falha)
    - [Transferência](#transferência)
      - [Exemplo de Request:](#exemplo-de-request-2)
      - [Possíveis Retornos:](#possíveis-retornos-7)
      - [O que acontece na Tentiva de Transferência:](#o-que-acontece-na-tentiva-de-transferência)
      - [Exemplo de Transferência com Sucesso](#exemplo-de-transferência-com-sucesso)
      - [Exemplo de Transferência com Falha](#exemplo-de-transferência-com-falha)
    - [Transação](#transação)
      - [Gerar Extrato](#gerar-extrato)
      - [Possíveis Retornos:](#possíveis-retornos-8)
      - [Ver Os Dados De Uma Transação](#ver-os-dados-de-uma-transação)
      - [Possíveis Retornos:](#possíveis-retornos-9)




## Sobre a API

Esta API oferece Endpoints para operações básicas de uma Conta Bancária.

### Feito Com
- [C# 7.3](https://docs.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-7)
- [ASP.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
- [NUnit](https://nunit.org/)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)



# Endpoints

- ## Conta Bancária

A base do programa, aliás, para se fazer um depósito, um saque, uma transferencia, ou até mesmo gerar um extrato você precisa que exista contas bancárias que armazem dados básicos como o Saldo, nome do cliente e o ID do cliente.

  - ### Criar Conta

A criação da conta se dá através da route:

    /api/contas

Faça um Post Request e passe os dados da Conta através do Body.
Apenas o Nome do Cliente é obrigatório, o ID vai ser gerado automaticamente se a classe for adicionada ao banco de dados e o Saldo pode ser 0 inicialmente.

  - #### Exemplo de Body:

```json
{
    "NomeCliente": "Luke Dias",
    "Saldo": "25000"
}
```

- #### Possíveis Retornos:
1. ##### Status 201: Created
    - Caso o Body seja válido, a conta será salva no banco de dados.
2. ##### Status 400: Bad Request
    - Caso o Body não seja válido, ele irá retornar uma mensagem de erro mostrando o motivo de não ter sido salvo.

- ### Ler Todas As Contas
A leitura de todas as contas se dá através da route:

    /api/contas

Faça um Get Request.

- #### Possíveis Retornos:
1. ##### Status 200: Ok
    - Caso existam Contas salvas no Banco de Dados, ele irá retornar uma lista com todas elas, além do Status Ok.
2. ##### Status 404: Not Found
    - Caso não existam Contas salvas no Banco de Dados.

- ### Ler Uma Conta
A leitura de apenas uma conta se dá através da route:

    /api/contas/{id}

Faça um Get Request e passe o ID da conta pela URL.

- #### Possíveis Retornos:
1. ##### Status 200: Ok
    - Caso ele encontre a conta de ID passado na URL ele retorna essa conta, além do Status Ok.
2. ##### Status 404: Not Found
    - Caso não encontre a Conta com o ID passado na URL.

- ### Atualizar Uma Conta
A atualização de uma conta se dá através da route:

    /api/contas/{id}

Faça um Put Request e passe o ID da conta pela URL, além disso, passe os dados da conta através do Body.
[Exemplo de Body](#exemplo-de-body)

- #### Possíveis Retornos:
1. ##### Status 200: Ok
    - Caso o Body seja válido, existe a Conta, e o ID da URL seja igual ao do Body, a conta será atualizada
2. ##### Status 400: Bad Request (ID no URL diferente do ID no Body)
    - Caso o ID na URL seja diferente do ID no Body, não será atualizado.
3. ##### Status 400: Bad Request (Problema no Body)
    - Caso o Body não seja válido, ele irá retornar uma mensagem de erro mostrando o motivo de não ter sido salvo.
4. ##### Status 404: Not Found
    - Caso não exista conta com o URL passado no banco de dados

- ### Deletar Uma Conta
Para deletar uma conta use o route:

    /api/contas/{id}

Faça um Delete Request e passe o ID da conta pela URL.

- #### Possíveis Retornos:
1. ##### Status 200: Ok
    - Caso ele encontre a lista do ID passado na URL, essa conta será deletada, além do retornar o Status Ok.
2. ##### Status 404: Not Found
    - Caso não encontre a Conta com o ID passado na URL.

- ## Deposito
Para fazer um depósito você precisa de uma Conta cadastrada no banco de dados.
Utilize o Route:

    /api/contas/{id}/deposito?valor=

Faça um Post Request e passe o ID e o  "valor" na URL.
Valor é quanto de dinheiro será depositado na conta.

- ### Exemplo de Request:

      /api/contas/1/deposito?valor=100
    
Você irá tentará fazer um deposito de valor = 100 na conta de ID = 1.

- ### Possíveis Retornos:
1. #### Status 200: Ok
    - Caso o valor passado seja maior que 0 e exista a conta com o ID passado na URL, a API vai fazer o Depósito
2. #### Status 404: Not Found
    - Caso não exista no banco de dados uma conta com o ID passado na URL
3. #### Status 400: Bad Request
    - Caso o valor passado na URL seja menor ou igual a 0.

- ### O que acontece no Depósito:
Primeiro ele faz o cálculo da taxa de Depósito, que é 1%, em cima do valor passado.
Em seguida ele acrescente esse valor no Saldo da conta.
E por último ele vai utilizar os dados do Depósito para gerar uma Transação.
Quando tudo isso terminar ele vai acrescentar a Transação ao Banco de Dados e atualizar a Conta com o Saldo Atual.

- ### Exemplo de Depósito:

       /api/contas/1/deposito?valor=100
    
- Supondo que existe a conta com ID = 1

Calcula a Taxa de 1% em cima do valor que é de 100, achando a taxa de valor 1
Adiciona o valor menos a taxa na conta, ou melhor, 99
Gera a Transação com todos os dados.
Salva os novos dados da Conta e adiciona a nova transação no banco de dados.
Retorna Staus Code 200: Ok

- ## Saque
Para fazer um saque você precisa de uma Conta cadastrada no banco de dados.
Utilize o Route:

    /api/contas/{id}/saque?valor

Faça um Post Request e passe o ID e o  "valor" na URL.
Valor é quanto de dinheiro será sacado da conta.

- ### Exemplo de Request:

         /api/contas/1/saque?valor=100
    
Você irá tentará fazer um saque de valor = 100 na conta de ID = 1.

- ### Possíveis Retornos:
1. #### Status 200: Ok
    - Caso o valor passado seja maior que 0 e exista a conta com o ID passado na URL, a API vai fazer a tentativa de saque
2. #### Status 404: Not Found
    - Caso não exista no banco de dados uma conta com o ID passado na URL
3. #### Status 400: Bad Request
    - Caso o valor passado na URL seja menor ou igual a 0.

- ### O que acontece na Tentiva de Saque:
Primeiro ele vai verificar duas coisas:
1. Se o valor da tentativa é menor ou igual a taxa
2. Se a conta tem o Saldo necessário para fazer esse Saque

Caso um dos dois seja verdade, não será sacado o dinheiro da conta, para que ela não fique com Saldo negativo, ou para o valor do saque depois da taxa não ser negativo.
Em seguida uma transação será gerada, com o resultado igual a "FALHA", para mostrar que ocorreu um erro durante o saque.

Caso nenhum dos casos sejam verdadeiros, ele irá sacar aquele valor da conta, sendo que do total, 4 será de taxa, e irá gerar uma Transação com o resultado igual a "SUCESSO".

No final de qualquer um dos dados ele irá retornar um Ok, e pedir pro usuário olhar o Extrato para saber se a transação teve ou não sucesso

- ### Exemplo De Saque Com Sucesso:

       /api/contas/1/saque?valor=100
    
1. Supondo que a Conta de ID = 1 exista
2. Supondo que a Conta de ID = 1 tenha um Saldo maior ou igual a 100

Irá testar para ver se valor é menor que a taxa, porém não é, e em seguida testará se tem saldo o suficiente, e realmente há.
Com isso o valor será subtraído do Saldo da Conta, sendo desses 100, 4 de taxa.
Em seguida será gerada uma transação com os dados que foram tirados da tentativa de Saque, essa transação terá resultado = "SUCESSO"

- ### Exemplo De Saque Com Falha:

      /api/contas/1/saque?valor=100
    
1. Supondo que a Conta de ID = 1 exista
2. Supondo que a Conta de ID = 1 tenha um Saldo menor que 100

Irá testar para ver se o valor é menor que a taxa, porém não é, e em seguida testará se tem saldo o suficiente, e verá que não tem Saldo o suficiente na conta para continuar o Saque. Ele não subtraíra da conta e irá gerar uma Transação com o resultado = "FALHA"

- ## Transferência
Para fazer uma Transferência você precisa de duas Contas cadastradas no banco de dados.
Utilize o Route:

    /api/contas/{id}/transferencia/{idDestino}?valor=

Faça um Post Request e passe o ID da conta de Origem e o ID da conta que receberá o valor e o próprio "valor" na URL.
Valor é quanto de dinheiro será transferidos de uma conta para a outra.

- ### Exemplo de Request:

      api/contas/1/transfrencia/2?valor=100
      
Você irá tentará fazer um transferência de valor = 100 da conta de ID = 1 para a conta de ID = 2.

- ### Possíveis Retornos:
1. #### Status 200: Ok
    - Caso o valor passado seja maior que 0 e existam as contas com os IDs passados na URL, a API vai fazer a tentativa de transferencia
2. #### Status 404: Not Found
    - Caso não seja encontrada uma das contas com os IDs passados na URL
3. #### Status 400: Bad Request
    - Caso o valor passado na URL seja menor ou igual a 0.

- ### O que acontece na Tentiva de Transferência:
Primeiro ele vai verificar duas coisas:
1. Se o valor da tentativa é menor ou igual a taxa
2. Se a conta tem o Saldo necessário para fazer essa Transferência

Caso um dos dois seja verdade, não será transferido o dinheiro da conta, para que ela não fique com Saldo negativo, ou para o valor da Transferencia depois da taxa não ser negativo.
Em seguida uma transação será gerada, com o resultado igual a "FALHA", para mostrar que ocorreu um erro durante o a Transferencia.

Caso nenhum dos casos sejam verdadeiros, ele irá subtrair aquele valor da Conta Origem e somar o valor menos a taxa da Conta Destino, essa taxa será de 1, e irá gerar uma Transação para cada conta com o resultado igual a "SUCESSO". Uma Transação será vinculada à conta Origem e a outra à conta Destino. 

- ### Exemplo de Transferência com Sucesso

      /api/contas/1/transferencia/2?valor=100
      
1. Supondo que a Conta de ID = 1 e conta de ID = 2 existam
2. Supondo que a Conta de ID = 1 tenha um Saldo maior ou igual a 100

Irá testar para ver se valor é menor que a taxa, porém não é, e em seguida testará se tem saldo o suficiente, e realmente há.
Com isso o valor será subtraído do Saldo da Conta Origem, e será somado na Conta Destino menos o 1 de taxa.
Em seguida serão geradas duas Transações com os dados que foram tirados da tentativa de Transferência, essa duas transações terão resultado = "SUCESSO", a vinculada à conta Origem terá tipo = "TRANSFERENCIA - ENVIO" e a vinculada à conta Destino terá o tipo = "TRANSFERENCIA - RECEBIMENTO"

- ### Exemplo de Transferência com Falha

      /api/contas/1/transferencia/2?valor=100
    
1. Supondo que a Conta de ID = 1 e conta de ID = 2 existam
2. Supondo que a Conta de ID = 1 tenha um Saldo menor que 100

Irá testar para ver se o valor é menor que a taxa, porém não é, e em seguida testará se tem saldo da conta Origem é o suficiente, e verá que não tem Saldo o suficiente na conta para continuar a Transferência. Ele não subtraíra da Conta Origem e irá gerar apenas uma Transação com o resultado = "FALHA"

- ## Transação
A transação é outra base da API, todas as operações geram uma Transação que é guardada no banco de dados, independente do fato dela ter sucesso ou não.

- Os dados principais da Transação são:
    - ***Data da Movimentação***: Para podermos gerar o Extrato dos últimos 30 dias essa informação é necessária
    - ***Tipo de Movimentação***: Para o usuário saber se entrou ou saiu dinheiro de sua conta
    - ***Resultado***: Para o usuário saber se a operação foi ou não bem sucedida
    - ***Valor Tentativa***: Qual valor o usuário passou naquela operação
    - ***Taxas***: Qual o valor total das taxas daquela Transação
    - ***ID da Conta***: Para poder passar essas informações pra conta específica que as fez

- ### Gerar Extrato
Para gerar o Extrato você precisa de uma Conta cadastrada no banco de dados e essa conta precisa já ter feita algum tipo de Transação antes.
Utilize o Route:

    /api/contas/{id}/transacoes?page=

Faça um Get Request e passe o ID da conta que deseja ver o extrato. Passar a página é opcional, caso não passe será mostrado os últimos 30 dias de transações. Cada página irá mostrar os 30 dias posteriores à página anterior, então a página 2 mostrará 30 dias a partir de 30 dias atrás, e assim em diante.

- ### Possíveis Retornos:
1. #### Status 200: Ok
    - Caso existam Transações no período de tempo que cobre aquela página, e caso exista uma Conta com o ID passado na URL.
2. #### Status 404: Not Found
    - Caso não existam transações no período de tempo que cobre uma página, ou não exista uma Conta o ID passado na URL.


- ### Ver Os Dados De Uma Transação
Para gerar ver os dados de uma única Transação você precisa de uma Conta Cadastrada no Banco de Dados e uma Transação vinculada à essa conta.
Utilize o Route:

    /api/contas/{id}/transacoes/{idTrasacao}

Faça um Get Request e passe o ID da conta que deseja ver a Transação, além disso passe o ID da Transação em si.

- ### Possíveis Retornos:
1. #### Status 200: Ok
    - Caso exista uma Conta com aquele ID e a Transação com o ID passado na URL esteja vinculado à essa conta.
2. #### Status 404: Not Found
    - Caso não exista uma Conta com aquele ID ou Transação com o ID passado na URL não esteja vinculado à essa conta.
