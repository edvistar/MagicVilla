﻿using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Datos
{
    public class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto {Id=1, Name="Vista a la Piscina", Ocupantes=3, MetrosCuadrados=50},
            new VillaDto {Id=2, Name="Vista a la Playa", Ocupantes=4, MetrosCuadrados=80}
        };
        
    }
}
