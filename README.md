# Desafio Alura

O desafio consiste no desenvolvimento de um RPA simples que realiza uma busca 
automatizada no site da Alura (https://www.alura.com.br/) e grava os resultados em um 
banco de dados.

## Índice

- [Sobre](#sobre)
- [Pré-requisitos](#pré-requisitos)

## Sobre

O desafio em si foi feito para avaliar o uso da ferramenta selenium no C#. Deste modo foi desenvolvido uma automação simples com Selenium e Dapper, no modelo DDD e com injeção de dependencia.

Acredito que essa automação poderia ter sido feita parte por API da Alura (https://cursos.alura.com.br/api/search?limit=1&query=#Termo#&page=1&isForum=false), mas por acreditar que essa não seria a maneira proposta, não fui a fundo nela.

Também seria possível ser feito por interceptação de request, mas acredito que o desenvolvimento ficaria um pouco mais complexo do que precisaria, tendo em vista que o site da Alura é bem simples, facilitando automações.

Por tanto, fiz um console apenas para testes da automação do site em Selenium. Acredito que em uma escala maior, poderia ter sido ferramentas de Lambdas, aliando a busca da API com a Lambda. Também poderia ter sido usado o Key vault para armazenamento da Connection String.

## Pré-requisitos

Para rodar 

- **Variável de Ambiente**: (CONNECTION_STRING_ALURA)

