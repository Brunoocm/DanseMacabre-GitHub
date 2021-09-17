using System.Collections;
using System.Collections.Generic;

public class BTSequence : BTNode
{
	public List<BTNode> children = new List<BTNode>();

	/* O 'Sequence' em Behaviour Tree (BT) tem a função de rodas todos os nós filhos até que um resulte -> FAILURE*/ 
	/* Portanto é utilizado quando queremos que os nós rodem em sequência baseados no sucesso dos anteriores*/
	/* Então ele é usado para gerar sequências de lógica mais complexas que precisam de verificações/funcionamentos anteriores*/
	public override IEnumerator Run(BehaviourTree bt)
	{
		status = Status.RUNNING;

		//Checa o status de todos os nodes filhos
		//Se algum dos filhos == FAILURE -> break; 
		foreach (BTNode node in children) {
			yield return bt.StartCoroutine(node.Run(bt));
			if (node.status == Status.FAILURE) {
				status = Status.FAILURE;
				break;
			}
		}
		
		//Aqui como o break não foi chamado todos os filhos rodaram com SUCESS
		if (status == Status.RUNNING) status = Status.SUCCESS;
	}
}
