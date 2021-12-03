const vector = ['url(/Utils/images/01.png)', 'url(/Utils/images/01.png)', 'url(/Utils/images/02.png)', 'url(/Utils/images/02.png)',
    'url(/Utils/images/03.png)', 'url(/Utils/images/03.png)', 'url(/Utils/images/04.png)', 'url(/Utils/images/04.png)', 'url(/Utils/images/05.png)', 'url(/Utils/images/05.png)',
    'url(/Utils/images/06.png)', 'url(/Utils/images/06.png)', 'url(/Utils/images/07.png)', 'url(/Utils/images/07.png)', 'url(/Utils/images/08.png)', 'url(/Utils/images/08.png)',
    'url(/Utils/images/09.png)', 'url(/Utils/images/09.png)'
];

let cartas_pares = [];
let cartas_abiertas = [];

class Juego {
    constructor() { }

    newBoard() {
        let out = "";
        mezclar(vector);
        for (let i = 0; i < vector.length; i++)
            out += `<div id=caja_${i}></div>`;
        document.getElementById('memory_board').innerHTML = out;
        for (let i = 0; i < vector.length; i++)
            document.getElementById(`caja_${i}`).addEventListener('click', this.getClick);
    }

    getClick() {
        let i = parseInt(this.getAttribute('id').substring(5)); // index para buscar el imagen en array
        // this la carta que tocamos
        if (cartas_pares.length < 4 && !cartas_abiertas.includes(this.id)) { //vertif si id de la carta ya esta abierta
            document.getElementById(`caja_${i}`).style.background = vector[i];
            if (cartas_pares.length === 0) guardar(this); // this es la carta que tocamos 
            else if (cartas_pares.length === 2 && cartas_pares[1] !== this.id) {
                guardar(this);
                if (cartas_pares[0] === cartas_pares[2]) {
                    cartas_abiertas.push(cartas_pares[1], cartas_pares[3]); // guardamos id de todas las cartas abiertas iguales
                    limpiar();
                    if (cartas_abiertas.length === 18) {
                        document.getElementById("fin").innerHTML = "¡¡Has logrado terminar el juego!! ¡¡Felicitaciones!!";
                        for (let i = 0; i < cartas_abiertas.length; i++)
                            document.getElementById("memory_board").removeChild(document.getElementById(`caja_${i}`));
                        document.getElementById("memory_board").style.height = "145px";
                        document.getElementById("memory_board").style.width = "145px";
                        document.getElementById("memory_board").style.background = "url('/Utils/images/fin.jpg')";
                        //fin juego
                    }
                } else setTimeout(volver, 700);
            }
        }
    }
}

// funcion que tira aleratoriamente los valores
const mezclar = (array) => {
    const tmp = () => Math.random() - 0.65;
    return array.sort(tmp);
}

const volver = () => {
    document.getElementById(cartas_pares[1]).style.background = "url('/Utils/images/cobertura.png')";
    document.getElementById(cartas_pares[3]).style.background = "url('/Utils/images/cobertura.png')";
    limpiar();
}

const guardar = (carta) => {
    cartas_pares.push(carta.style.background, carta.id);
    /* guardamos imagen para poder comparar si son iguales,
       y el id para dar vuelta si son diferentes */
}

const limpiar = () => {
    cartas_pares.length = 0;
}
const iniciar = () => {
    cartas_pares = [];
    cartas_abiertas = [];
    juego = new Juego();
    juego.newBoard();
}

iniciar();