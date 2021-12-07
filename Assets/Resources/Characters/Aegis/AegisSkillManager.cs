using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe responsável por ser o "CONTROLE" de habilidades do jogador.
public class AegisSkillManager : MonoBehaviour
{
    public void VerifyPhase(int skillId)
    {
        UseActionClass message = new UseActionClass();
        message.setHabIndex(skillId);
        ClientManager.GameRoom.Send("CheckAction", message);
    }
}
