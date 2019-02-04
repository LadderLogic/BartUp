﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Bartup.Contracts;
//
//    var bartResponse = BartResponse.FromJson(jsonString);

namespace Bartup.Contracts
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BartResponse
    {
        [JsonProperty("?xml")]
        public Xml Xml { get; set; }

        [JsonProperty("root")]
        public Root Root { get; set; }
    }

    public partial class Root
    {
        [JsonProperty("@id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("uri")]
        public UriClass Uri { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("station")]
        public Station[] Station { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public partial class Station
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("abbr")]
        public string Abbr { get; set; }

        [JsonProperty("etd")]
        public Etd[] Etd { get; set; }
    }

    public partial class Etd
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("limited")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Limited { get; set; }

        [JsonProperty("estimate")]
        public Estimate[] Estimate { get; set; }
    }

    public partial class Estimate
    {
        [JsonProperty("minutes")]
        public string Minutes { get; set; }

        [JsonProperty("platform")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Platform { get; set; }

        [JsonProperty("direction")]
        public Direction Direction { get; set; }

        [JsonProperty("length")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Length { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("hexcolor")]
        public Hexcolor Hexcolor { get; set; }

        [JsonProperty("bikeflag")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Bikeflag { get; set; }

        [JsonProperty("delay")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Delay { get; set; }
    }

    public partial class UriClass
    {
        [JsonProperty("#cdata-section")]
        public Uri CdataSection { get; set; }
    }

    public partial class Xml
    {
        [JsonProperty("@version")]
        public string Version { get; set; }

        [JsonProperty("@encoding")]
        public string Encoding { get; set; }
    }

    public enum Color { Blue, Green, Red, Yellow };

    public enum Direction { North, South };

    public enum Hexcolor { Ff0000, Ffff33, The0099Cc, The339933 };

    public partial class BartResponse
    {
        public static BartResponse FromJson(string json) => JsonConvert.DeserializeObject<BartResponse>(json, Bartup.Contracts.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BartResponse self) => JsonConvert.SerializeObject(self, Bartup.Contracts.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ColorConverter.Singleton,
                DirectionConverter.Singleton,
                HexcolorConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class ColorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Color) || t == typeof(Color?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "BLUE":
                    return Color.Blue;
                case "GREEN":
                    return Color.Green;
                case "RED":
                    return Color.Red;
                case "YELLOW":
                    return Color.Yellow;
            }
            throw new Exception("Cannot unmarshal type Color");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Color)untypedValue;
            switch (value)
            {
                case Color.Blue:
                    serializer.Serialize(writer, "BLUE");
                    return;
                case Color.Green:
                    serializer.Serialize(writer, "GREEN");
                    return;
                case Color.Red:
                    serializer.Serialize(writer, "RED");
                    return;
                case Color.Yellow:
                    serializer.Serialize(writer, "YELLOW");
                    return;
            }
            throw new Exception("Cannot marshal type Color");
        }

        public static readonly ColorConverter Singleton = new ColorConverter();
    }

    internal class DirectionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Direction) || t == typeof(Direction?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "North":
                    return Direction.North;
                case "South":
                    return Direction.South;
            }
            throw new Exception("Cannot unmarshal type Direction");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Direction)untypedValue;
            switch (value)
            {
                case Direction.North:
                    serializer.Serialize(writer, "North");
                    return;
                case Direction.South:
                    serializer.Serialize(writer, "South");
                    return;
            }
            throw new Exception("Cannot marshal type Direction");
        }

        public static readonly DirectionConverter Singleton = new DirectionConverter();
    }

    internal class HexcolorConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Hexcolor) || t == typeof(Hexcolor?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "#0099cc":
                    return Hexcolor.The0099Cc;
                case "#339933":
                    return Hexcolor.The339933;
                case "#ff0000":
                    return Hexcolor.Ff0000;
                case "#ffff33":
                    return Hexcolor.Ffff33;
            }
            throw new Exception("Cannot unmarshal type Hexcolor");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Hexcolor)untypedValue;
            switch (value)
            {
                case Hexcolor.The0099Cc:
                    serializer.Serialize(writer, "#0099cc");
                    return;
                case Hexcolor.The339933:
                    serializer.Serialize(writer, "#339933");
                    return;
                case Hexcolor.Ff0000:
                    serializer.Serialize(writer, "#ff0000");
                    return;
                case Hexcolor.Ffff33:
                    serializer.Serialize(writer, "#ffff33");
                    return;
            }
            throw new Exception("Cannot marshal type Hexcolor");
        }

        public static readonly HexcolorConverter Singleton = new HexcolorConverter();
    }
}
