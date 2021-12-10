function main() {
    mostrarTablaRanking()
}

const gridRanking = new gridjs.Grid()

function mostrarTablaRanking() {
    const columnas = [
        { id: "Nombre",   name: "Nombre" },
        { id: "Precio", name: "Precio" },
        { id: "fechaIngreso", name: "fechaIngreso" },
        { id: "fechaUltimaVenta", name: "fechaUltimaVenta" },
        { id: "CantidadVendidos", name: "CantidadVendidos" },
    ]
    const contenedor = document.getElementById("tablaProductosConFiltro")
    mostrarTabla(gridRanking, columnas, contenedor, actualizarTablaPorRanking)
}


