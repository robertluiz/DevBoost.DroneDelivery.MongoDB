# Devboost.Pagamento.Kafka
Drone Delivery

- Realizando autenticação via JWT com o uso do tipo "Bearer Token"
- Desenvolvido usando DDD e CQRS
- Desenvolvido uma API que recebe pedidos, lista drones e seus pedidos associados
- Cria 7 drones automaticamente na base caso ainda não exista nada cadastrado
- Desenvolvido outra API que inicia a entrega do pedido através de algum drone que esteja disponível
- Foi usado para o banco de dados o framework "ServiceStack.OrmLite" que fornece o "CodeFirst"
 -- Se as tabelas ainda não existirem no banco, aplica ela no momento que a aplicação subir



## Exemplo de envio de Pedido no Kafka

É preciso rodar o docker-componse que está ná raiz do projeto para instalar o kafka e o rest-proxy.

#### Instruções para o Postman

- No postman, na guia de Headers configurar o seguinte Contente-Type application/vnd.kafka.json.v2+json
- Na aba Body, colocar o formato, Raw e após JSON.

Post http://localhost:8082/topics/delivery-pedido

 



```
{
    "records": [
        {
            "key": "",
            "value": {
                "peso": 1000,
                "valor": 25,
                "bandeiracartao": "Visa",
                "nomecartao": "Robert Luiz",
                "numerocartao": "6277801223574589",
                "datavalidadecartao": "2022-03-19",
                "CodSegurancaCartao": "752",
                "TipoCartao": "Credito"
            }
        }
    ]
}
```