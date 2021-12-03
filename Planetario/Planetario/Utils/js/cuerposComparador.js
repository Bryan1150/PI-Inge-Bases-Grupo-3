"use strict";

const lightSpeed = 299792458; // meter (per second)
const solarMass = 1.98855 * Math.pow( 10, 30 ); // kilograms
const gravity = 6.67408 * Math.pow( 10, -11 );

function calcSchwarzschildRadius( nbSolarMass ) {
	return ( 2 * gravity * nbSolarMass * solarMass ) / ( lightSpeed * lightSpeed );
}

const Space = {
	solarSystem: {
		name: "Cuerpos del Sistema Solar",
		objects: {
			sun:       { name: "Sol",       diameter: 1391016, color: "#fff5f1", type: "yellow-dwarf-star", hasImg: true, aka: "Centro del Sistema Solar" },
			mercury:   { name: "Mercurio",  diameter:    4879, color: "#828086", type: "iron-planet",       hasImg: true, aka: "Planeta más cercano al Sol" },
			venus:     { name: "Venus",     diameter:   12104, color: "#d79f3c", type: "rocky-planet",      hasImg: true, aka: "Segundo planeta más cercano al Sol" },
			earth:     { name: "Tierra",    diameter:   12742, color: "#2c3789", type: "rocky-planet",      hasImg: true, aka: "Tercer planeta más cercano al Sol" },
			mars:      { name: "Marte",     diameter:    6779, color: "#ee7f5a", type: "rocky-planet",      hasImg: true, aka: "Cuarto planeta más cercano al Sol" },
			jupiter:   { name: "Jupiter",   diameter:  139822, color: "#a4906f", type: "gaz-planet",        hasImg: true, aka: "Quinto planeta más cercano al Sol" },
			saturn:    { name: "Saturno",   diameter:  116464, color: "#e2cb84", type: "gaz-planet",        hasImg: true, aka: "Sexto planeta más cercano al Sol" },
			uranus:    { name: "Urano",     diameter:   50724, color: "#c1e7ea", type: "gaz-planet",        hasImg: true, aka: "Séptimo planeta más cercano al Sol"},
			neptune:   { name: "Neptuno",   diameter:   49244, color: "#3d66fa", type: "gaz-planet",        hasImg: true, aka: "Octavo planeta más cercano al Sol" },
			pluto:     { name: "Pluto",     diameter:    2377, color: "#866143", type: "dwarf-planet",      hasImg: true, aka: "No es considerado un planeta del Sistema Solar" },
		}
	},
	estrellas: {
		name: "Satélites",
		objects: {
			moon:      { name: "Luna (Satélite de la Tierra)",     diameter:    3474, color: "#7f7978", type: "satellite", hasImg: true, aka: "Único satélite de la Tierra" },
			ganymede:  { name: "Ganymede (Satélite de Jupiter)",   diameter:    5268, color: "#8d7c69", type: "satellite", hasImg: true, aka: "Satélite más grande de Júpiter" },
			callisto:  { name: "Callisto (Satélite de Jupiter)",   diameter:    4820, color: "#91775e", type: "satellite", hasImg: true, aka: "Segundo satélite más grande de Júpiter" },
			io:        { name: "Io (Satélite de Jupiter)",         diameter:    3643, color: "#fbf680", type: "satellite", hasImg: true, aka: "Tercer satélite más grande de Júpiter" },
			europa:    { name: "Europa (Satélite de Jupiter)",     diameter:    3122, color: "#9c7047", type: "satellite", hasImg: true, aka: "Cuarto satélite más grande de Júpiter" },
			titan:     { name: "Titan (Satélite de Saturno)",      diameter:    5151, color: "#d6c359", type: "satellite", hasImg: true, aka: "Satélite más grande de Saturno" },
			rhea:      { name: "Rhea (Satélite de Saturno)",       diameter:    1528, color: "#b2b2b2", type: "satellite", hasImg: true, aka: "Segundo satélite más grande de Saturno" },
			iapetus:   { name: "Iapetus (Satélite de Saturno)",    diameter:    1469, color: "#c3c1b2", type: "satellite", hasImg: true, aka: "Tercer satélite más grande de Saturno" },
			dione:     { name: "Dione (Satélite de Saturno)",      diameter:    1123, color: "#c1c1c1", type: "satellite", hasImg: true, aka: "Cuarto satélite más grande de Saturno" },
			tethys:    { name: "Tethys (Satélite de Saturno)",     diameter:    1062, color: "#b0afaf", type: "satellite", hasImg: true, aka: "Quinto satélite más grande de Saturno" },
			enceladus: { name: "Enceladus (Satélite de Saturno)",  diameter:     504, color: "#aeaeae", type: "satellite", hasImg: true, aka: "Sexto satélite más grande de Saturno" },
			mimas:     { name: "Mimas (Satélite de Saturno)",      diameter:     396, color: "#8b8b8b", type: "satellite", hasImg: true, aka: "Séptimo satélite más grande de Saturno" },
			titania:   { name: "Titania (Satélite de Urano)",      diameter:    1577, color: "#c3b7ad", type: "satellite", hasImg: true, aka: "Satélite más grande de Urano" },
			oberon:    { name: "Oberon (Satélite de Urano)",       diameter:    1523, color: "#ae9b94", type: "satellite", hasImg: true, aka: "Segundo satélite más grande de Urano" },
			umbriel:   { name: "Umbriel (Satélite de Urano)",      diameter:    1169, color: "#3d3d3d", type: "satellite", hasImg: true, aka: "Tercer satélite más grande de Urano" },
			ariel:     { name: "Ariel (Satélite de Urano)",        diameter:    1158, color: "#b6b6b6", type: "satellite", hasImg: true, aka: "Cuarto satélite más grande de Urano" },
			miranda:   { name: "Miranda (Satélite de Urano)",      diameter:     472, color: "#bbbbbb", type: "satellite", hasImg: true, aka: "Quinto satélite más grande de Urano" },
			triton:    { name: "Triton (Satélite de Neptuno)",     diameter:    2707, color: "#aea5a6", type: "satellite", hasImg: true, aka: "Satélite más grande de Neptuno" },
		}
	},
};

const ObjectByName =
	Object.values( Space ).reduce( ( obj, cate ) => (
		Object.entries( cate.objects ).reduce(
			( obj, [ id, spaceObj ] ) => {
				spaceObj.id = id;
				obj[ id ] = spaceObj;
				return obj;
			},
			obj )
	), {} );

Object.values( Space.blackholes.objects ).forEach( bh => {
	bh.diameter = Math.round( 2 * calcSchwarzschildRadius( bh.nbSolarMass ) / 1000 );
} );
