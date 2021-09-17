using System.Collections;
using System.Collections.Generic;

public class BTSelector : BTNode
{
	public List<BTNode> children = new List<BTNode>();

	/* O 'selector' em Behaviour Tree( BT ) tem a função de selecionar e rodar o primeiro filho que resulte -> SUCESS */
	/* Portanto ele é utilizado para quando não queremos que todos os nodes filhos rodem*/
	/* Então é usado para controlar a execução de acordo com a ordem de prioridade dada no planejamento da BT*/
	/* Já que ele checa os nós filhos ordenadamente*/
	public override IEnumerator Run(BehaviourTree bt)
	{
		status = Status.RUNNING;

		//Checa o status de todos os nodes filhos
		//Se algum filho == SUCESS -> Status.SUCSESS
		foreach (BTNode node in children) {
			yield return bt.StartCoroutine(node.Run(bt));
			if (node.status == Status.SUCCESS) {
				status = Status.SUCCESS;
				break;
			}
		}

		//Como nenhum dos nós filhos resultou em SUCESS, status -> Status.FAILURE
		if (status == Status.RUNNING) status = Status.FAILURE;
	}
}