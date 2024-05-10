using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inazuma_Eleven_Victory_Road_Beta.InazumaEleven_V1._0._0
{
    internal class Ability
    {
        /*
         *  66 3F 40 29: Valor propio de foco +3% en campo contrario
            F0 0F 47 5E: Valor propio de Foco +5% en campo contrario
            99 9A 3E CD: Valor de Disputa +3% para jugadores de distintas posiciones
            A5 6C 6D 02: Cuando la Tensión está por debajo del 30%, valor de Foco de MC +10%
            40 E5 AA 62: Cuando la Tensión está por debajo del 30%, AT de tiro del equipo +20%
            87 73 EB 2D: Cuando un jugador del mismo elemento está cerca, AT propio de tiro +3%
            E0 98 1A 7F: Cuando un jugador del mismo elemento está cerca, AT propio de tiro +5%
            C6 42 F0 34: AT de tiro +3% para jugadores de la misma posición
            A4 B5 84 EC: Cuando el rival comete una falta, Poder de Afinidad +4%
            55 90 EE 4C: Hasta que el equipo reciba una falta, AT/DF del equipo +15%
            43 0D 7E E1: Cuando un jugador de otro elemento está cerca, AT propio de tiro +3%
            D2 10 C1 71: Cuando un jugador de otro elemento está cerca, AT propio de tiro +5%
            57 5F 4F A4: Valor de Foco +3% para jugadores de distintos elementos
            CE C8 29 FC: Cuando el equipo dispersa a los oponentes esprintando, Tensión +2%
            58 F8 2E 8B: Tasa de faltas del equipo -10% cuando se está fuera del Área
            11 43 EC 5A: DF del Muro +3% para jugadores cercanos
            19 C9 35 92: Hasta que el equipo reciba una falta, DF del Muro +15%
            C5 AA 24 B7: Valor propio de Disputa +3% cuando se está fuera del Área
            54 B7 9B 27: Valor propio de Disputa +5% cuando se está fuera del Área
            B5 FB 30 23: Valor de Disputa +3% para jugadores de la misma posición
            97 E4 B6 0C: Cuando el equipo pasa, Poder de Afinidad ganado +40%
            FC 40 F2 F2: Si el Poder de Afinidad es 20% o más, valor de Disputa del equipo +20%
            76 A8 1D 08: Cuando un jugador del mismo elemento está cerca, valor propio de Foco +3%
            AC 3F 5D 24: AT de tiro +3% para jugadores cercanos
            B1 3E 5C 47: La tasa de obtención de objetos raros del equipo aumenta un 25%
            3E CA 36 37: Cuando se tienen los mismos goles o menos, tasa de brecha del equipo +20%
            83 B6 87 49: Tensión necesaria para brecha del equipo -25%
            E9 CB 2A 59: Valor propio de Foco +3% cuando se está fuera del Área
            53 9A 23 C0: Valor propio de Foco +5% cuando se está fuera del Área
            F4 CA 2B 3A: AT de tiro +3% para jugadores del mismo elemento
            6A 70 F5 85: Si el Poder de Afinidad es 20% o más, valor de Disputa del equipo +10%
            01 D4 B1 7B: Cuando el equipo pasa, Poder de Afinidad ganado +60%
            37 99 06 11: Cuando un jugador de otro elemento está cerca, DF propia del Muro +3%
            8D C8 0F 88: Cuando un jugador de otro elemento está cerca, DF propia del Muro +5%
            88 D4 8A 02: Hasta que el equipo reciba una falta, DF del Muro +10%
            32 85 83 9B: Cuando el equipo se dispersa, Poder de Afinidad +6%
            16 6E 54 BD: Valor de Foco +3% para jugadores cercanos
            FD 99 1B 1C: Cuando la Tensión está al 50% o más, valor de Foco del equipo +10%
            72 6D 71 6C: Cuando el rival pasa durante Foco, drena un 15% de Tensión
            1B F8 08 FF: Valor propio de Foco +3% en campo propio
            0F AA 39 BA: Valor de Foco +3% para jugadores de distintas posiciones
            27 0E 5B 30: La tasa de obtención de objetos comunes del equipo aumenta un 25%
            C8 3C 78 F5: Cuando el equipo gana en Foco o Disputa, Tensión +5%
            23 CB 37 54: Valor de Foco +3% para jugadores de la misma posición
            B8 6D 6C 61: Valor propio de Foco +5% en campo propio
            C1 6F 48 D3: Valor de Disputa +3% para jugadores de distintos elementos
            E4 5D 76 1B: Cuando el rival pasa durante Foco, drena un 10% de Tensión
            5E 0C 7F 82: Cuando el equipo gana en Foco o Disputa, Tensión +10%
            7B 3E 41 4A: DF del Muro +3% para jugadores del mismo elemento
            89 0D 63 EC: Cuando la Tensión está por debajo del 30%, valor de Foco de DF +10%
            2A 98 07 72: Cuando la Tensión está por debajo del 30%, daño a los PP-20%
            93 21 DA 68: AT propio de tiro +3% en campo contrario
            12 AB 38 D9: Si la bonif. de brecha es 50% o más, tasa perfor. de Muro del equipo +7%
            9D 5F 52 A9: Tasa de brecha del equipo +30%
            7F FB 2D 2E: Valor de Foco del equipo +5% en campo contrario
            C3 A0 E9 3B: Tras una sustitución, AT del jugador sustituto +3% durante 15s
            79 F1 E0 A2: Tras una sustitución, AT del jugador sustituto +5% durante 15s
            F6 05 8A D2: Durante la primera mitad, AT del equipo +10%
            67 18 35 42: Durante la primera mitad, AT del equipo +15%
            6B A9 1C 6B: Cuando la Tensión está al 50% o más, valor de Foco del equipo +20%
            E5 84 9F F5: Tasa de faltas del equipo -5% al esprintar
            2B 41 EE 9C: Cuando el equipo dispersa a los oponentes esprintando, Tensión +3%
            4E 9B 22 A3: Valor de Foco +3% para jugadores del mismo elemento
            8F F9 32 E5: Hasta que el equipo reciba una falta, AT/DF del equipo +10%
            1E E4 8D 75: Cuando el rival comete una falta, Poder de Afinidad +6%
            5F D5 96 6C: Tasas de faltas del equipo -10% al esprintar
            6F 6C 70 0F: Cuando un jugador del mismo elemento está cerca, DF propia del Muro +3%
            D5 3D 79 96: Cuando un jugador del mismo elemento está cerca, DF propia del Muro +5%
            BC A8 00 05: Cuando la Tensión está por debajo del 30%, daño a los PP -15%
            1F 3D 64 9B: Cuando la Tensión está por debajo del 30%, valor de Foco de DF +20%
            5A C9 13 E6: Cuando un jugador del mismo elemento está cerca, valor propio de Disputa +3%
            F9 5C 77 78: Cuando un jugador del mismo elemento está cerca, valor propio de Disputa +5%
            D8 AB 25 D4: Valor de Disputa +3% para jugadores del mismo elemento
            91 10 E7 05: Cuando el equipo se dispersa, Poder de Afinidad +4%
            2E 5D 6B 16: Valor propio de disputa +3% en campo propio
            94 0C 62 8F: Valor propio de disputa +5% en campo propio
            90 C9 0E EB: Cuando la Tensión está por debajo del 30% AT/DF ataque duro (equipo) +10%
            50 72 F7 43: DF del Muro +3% para jugadores de distintos elementos
            3A 0F 5A 53: DF del Muro +3% para jugadores de distintas posiciones
            73 B4 98 82: Cuando el equipo pierde una Disputa,
            BD 71 E9 EB: Al dispersar rivales esprintando, AT/DF del sig. ataque duro de equipo +5%
            0B 6F 55 DE: Tasa de brecha del equipo +20%
            A8 FA 31 40: Cuando se tienen los mismos goles o menos, tasa de brecha del equipo +30%
            15 86 80 3E: Tensión necesaria para hacer brecha de equipo -15%
            84 9B 3F AE: Si la bonif. de brecha es 50% o mas, tasa perfor. de Muro del equipo +13%
            ED 0E 46 3D: AT de tiro +3% para jugadores de distintos elementos
            46 11 FB 6B: Si el Poder de Afinidad es 20% o más, AT de Tiro directo del equipo +7%
            02 3C 65 F8: Valor de Foco del equipo +5% en campo propio
            EF C1 E7 D5: Tras una sustitución, DF del jugador sustituto +3% durante 15s
            4C 54 83 4B: Tras una sustitución, DF del jugador sustituto +5% durante 15s
            F1 28 32 35: Durante la segunda mitad, DF del equipo +10%
            14 A1 F5 55: Durante la segunda mitad, DF del equipo +15%
            D1 F8 15 F2: Cuando la Tensión está al 100% AT de tiro del equipo +40
            4A 5E 4E C7: Valor propio de Disputa +3% en campo contrario
            DC 6E 49 B0: Valor propio de Disputa +5% en campo contrario
            D6 D5 AD 15: Cuando la Tensión está por debajo del 30%, AT de tiro del equipo +10%
            33 5C 6A 75: Cuando la Tensión está por debajo del 30%, valor de Foco de MC +20%
            06 F9 09 9C: Cuando la Tensión está por debajo del 30% AT/DF ataque duro (equipo) +15%
            47 C8 12 85: Cuando la Tensión está al 100% AT de tiro del equipo +60%
            C9 E5 91 1B: Tasa de faltas del equipo -5% cuando se está fuera del Área
            07 20 E0 72: Al dispersar a rivales esprintando, AT/DF del sig. ataque duro del equipo +10%
            44 20 C6 06: Cuando un jugador de otro elemento está cerca, valor propio de Foco +3%
            D0 21 FC 1C: Si el poder de la Afinidad es 20% o más, AT de tiro directo del equipo +13%
            80 5E 53 CA: Valor de Disputa +3% para jugadores cercanos
            C2 87 9C 50: Valor de Foco del equipo +5% cuando se está fuera del Área
            DA 64 84 3C: Enfriamiento de Tácticas Especiales -10%
            60 35 8D A5: Enfriamiento de Tácticas Especiales -15%
         * */
    }
}
