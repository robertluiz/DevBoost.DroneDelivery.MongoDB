Funcionalidade: Na criação do pedido selecionar o drone
				que esteja com disponibilidade, capacidade e autonômia suficiente
				com base nos parâmetros de entrada do pedido
				tais como peso e distância de entrega do comprador

Cenario: Na criação do pedido selecionar o drone com base na sua disponibilidade, capacidade e autonômia
	Dado um pedido com peso total de 8000 gramas
	E com destino de entrega com distância prevista para 3.500 metros	
	Quando o pedido for inserido o drone selecionado deverá ao final corresponder aos critérios definidos
	Entao se o pedido foi inserido corretamente a seleção do drone deve ser True 