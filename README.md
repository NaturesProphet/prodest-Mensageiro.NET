[![Commitizen friendly](https://img.shields.io/badge/commitizen-friendly-brightgreen.svg)] (http://commitizen.github.io/cz-cli/) [![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-yellow.svg)](https://conventionalcommits.org)

# Mensageiro

Pequeno serviço em DotNet Core 2.1 (C#) que fica permanentemente ouvindo o topico da ceturb na geocontrol e repassando tudo o que rola por lá para nosso RabbitMQ.

## Variaveis de ambiente para o Docker
Aqui está a lsita de todas as variáveis configuráveis do Mensageiro.  


```bash
APACHE_URL_CONNECTION                       # string de conexão do apache, com host e porta
Default: activemq:tcp://localhost:61613

APACHE_USER                                 # Usuário para conectar ao apache
Default: guest

APACHE_PASSWORD                             # Senha para conectar ao apache
Default: guest

APACHE_TOPIC                                # Topico para ser ouvido no apache
Default: TOPICO

RABBIT_URL_CONNECTION                       # string de conexão com o Rabbit
Default: localhost

RABBIT_TOPIC                                # Topico onde as mensagens são enviadas ao rabbit
Default: CETURB

RABBIT_ROUTING_KEY                          # Chave de roteamento do historico no rabbit
Default: CETURB

RABBIT_ROUTING_KEY_MONGO                    # Chave de roteamento do real-time no rabbit
Default: mongo
```