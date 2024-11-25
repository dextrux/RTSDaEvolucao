using System.Collections.Generic;
using UnityEngine;
public enum Alerta
{
    Temperatura, //|TI - Tr| > 15
    Umidade, //|UI - Ur| > 38
    Calor, //Se houver uma situação de alerta de temperatura e TI > Tr
    Frio, //Se houver uma situação de alerta de temperatura e TI < Tr
    Fome, //Se a barra de fome estiver abaixo de 25% do seu total
    Cansaço, //Se a barra de energia estiver abaixo de 25% do seu total
    Ressecaçao, //Se houver uma situação de alerta de umidade e UI > Ur
    Desconforto //Se houver uma situação de alerta de umidade e UI < Ur
}

