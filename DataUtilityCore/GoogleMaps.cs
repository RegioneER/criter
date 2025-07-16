using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Linq;
using System.Net;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace DataUtilityCore.Google
{
    public class GoogleMaps
    {
        public static InformazioniPercorso CalcolaDistanzaPercorso(string partenza, string arrivo)
        {
            #region esempio
            /*
                http://maps.googleapis.com/maps/api/distancematrix/json?origins=38034,italia&destinations=38122,italia&mode=driving&language=it-IT&sensor=false
                {
                   "destination_addresses" : [ "38122 Trento, Italia" ],
                   "origin_addresses" : [ "38034 Cembra TN, Italia" ],
                   "rows" : [
                      {
                         "elements" : [
                            {
                               "distance" : {
                                  "text" : "26,4 km",
                                  "value" : 26443
                               },
                               "duration" : {
                                  "text" : "38 min",
                                  "value" : 2288
                               },
                               "status" : "OK"
                            }
                         ]
                      }
                   ],
                   "status" : "OK"
                }
             */
            #endregion

            InformazioniPercorso toReturn = new InformazioniPercorso();
            WebClient wc = new WebClient();
            wc.Proxy = WebRequest.DefaultWebProxy;
            wc.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                byte[] data = wc.DownloadData(String.Format(@"https://maps.googleapis.com/maps/api/distancematrix/json?origins={0}&destinations={1}&mode=driving&language=it-IT&key={2}&sensor=false", partenza, arrivo, ConfigurationManager.AppSettings["GoogleApiKey"]));
                JavaScriptSerializer ser = new JavaScriptSerializer();
                toReturn.RawInfo = UTF8Encoding.UTF8.GetString(data);
                toReturn.Path = ser.Deserialize<GooglePath>(toReturn.RawInfo);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        // handle the 404 here
                    }
                }
                else if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    //handle name resolution failure
                }
            }

            return toReturn;
        }

        //public static InformazioniPercorso GetGeocoding(string indirizzo)
        //{
        //    #region esempio
        //    /*
        //        http://maps.googleapis.com/maps/api/geocode/json?address=Via%20Ferruccio%20Parri,40128%20Bologna,Italia&sensor=false
        //        {
        //            "results" : [
        //                {
        //                    "address_components" : [
        //                    {
        //                        "long_name" : "Via Ferruccio Parri",
        //                        "short_name" : "Via Ferruccio Parri",
        //                        "types" : [ "route" ]
        //                    },
        //                    {
        //                        "long_name" : "Bologna",
        //                        "short_name" : "Bologna",
        //                        "types" : [ "locality", "political" ]
        //                    },
        //                    {
        //                        "long_name" : "Bologna",
        //                        "short_name" : "BO",
        //                        "types" : [ "administrative_area_level_2", "political" ]
        //                    },
        //                    {
        //                        "long_name" : "Emilia-Romagna",
        //                        "short_name" : "Emilia-Romagna",
        //                        "types" : [ "administrative_area_level_1", "political" ]
        //                    },
        //                    {
        //                        "long_name" : "Italia",
        //                        "short_name" : "IT",
        //                        "types" : [ "country", "political" ]
        //                    },
        //                    {
        //                        "long_name" : "40128",
        //                        "short_name" : "40128",
        //                        "types" : [ "postal_code" ]
        //                    }
        //                    ],
        //                    "formatted_address" : "Via Ferruccio Parri, 40128 Bologna, Italia",
        //                    "geometry" : {
        //                    "bounds" : {
        //                        "northeast" : {
        //                            "lat" : 44.51281660,
        //                            "lng" : 11.35694740
        //                        },
        //                        "southwest" : {
        //                            "lat" : 44.51257760,
        //                            "lng" : 11.3567260
        //                        }
        //                    },
        //                    "location" : {
        //                        "lat" : 44.5127250,
        //                        "lng" : 11.35694740
        //                    },
        //                    "location_type" : "GEOMETRIC_CENTER",
        //                    "viewport" : {
        //                        "northeast" : {
        //                            "lat" : 44.51404608029151,
        //                            "lng" : 11.35818568029150
        //                        },
        //                        "southwest" : {
        //                            "lat" : 44.51134811970851,
        //                            "lng" : 11.35548771970850
        //                        }
        //                    }
        //                    },
        //                    "types" : [ "route" ]
        //                }
        //            ],
        //            "status" : "OK"
        //        }
        //     */
        //    #endregion

        //    InformazioniPercorso toReturn = new InformazioniPercorso();
        //    WebClient wc = new WebClient();
        //    wc.Proxy = WebRequest.DefaultWebProxy;
        //    wc.Credentials = CredentialCache.DefaultCredentials;

        //    try
        //    {
        //        byte[] data = wc.DownloadData(String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}&sensor=false", indirizzo, ConfigurationManager.AppSettings["GoogleApiKey"]));
        //        JavaScriptSerializer ser = new JavaScriptSerializer();
        //        toReturn.RawInfo = System.Text.UTF8Encoding.UTF8.GetString(data);
        //        toReturn.Path = ser.Deserialize<GooglePath>(toReturn.RawInfo);
        //    }
        //    catch (WebException ex)
        //    {
        //        if (ex.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            if (((HttpWebResponse) ex.Response).StatusCode == HttpStatusCode.NotFound)
        //            {
        //                // handle the 404 here
        //            }
        //        }
        //        else if (ex.Status == WebExceptionStatus.NameResolutionFailure)
        //        {
        //            //handle name resolution failure
        //        }
        //    }

        //    return toReturn;

        //}

        public static GeocoderLocation GetGeocodingAddress(string indirizzo)
        {
            WebRequest request = WebRequest.Create(String.Format(@"https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}&sensor=false", indirizzo, ConfigurationManager.AppSettings["GoogleApiKey"]));
            if (bool.Parse(ConfigurationManager.AppSettings["ProxyEnabled"]))
            {
                request.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyUsername"], ConfigurationManager.AppSettings["ProxyPassword"]);

                WebProxy webProxy = new WebProxy(ConfigurationManager.AppSettings["ProxyAddress"], true)
                {
                    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["ProxyUsername"], ConfigurationManager.AppSettings["ProxyPassword"]),
                    UseDefaultCredentials = false
                };

                request.Proxy = webProxy;
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        XDocument document = XDocument.Load(new StreamReader(stream));

                        XElement statusElement = document.Descendants("status").FirstOrDefault();
                        XElement longitudeElement = document.Descendants("lng").FirstOrDefault();
                        XElement latitudeElement = document.Descendants("lat").FirstOrDefault();

                        if (longitudeElement != null && latitudeElement != null)
                        {
                            return new GeocoderLocation
                            {
                                Status = statusElement.Value,
                                Longitude = Double.Parse(longitudeElement.Value, CultureInfo.InvariantCulture),
                                Latitude = Double.Parse(latitudeElement.Value, CultureInfo.InvariantCulture)
                            };
                        }
                    }
                }

            }
            catch (WebException wex)
            {

            }
            
            return null;
        }

        public static string GetZipCodeFromAddress(string address)
        {
            string ZipCode = string.Empty;

            if (!string.IsNullOrEmpty(address))
            {
                WebClient wc = new WebClient();
                wc.Proxy = WebRequest.DefaultWebProxy;
                wc.Credentials = CredentialCache.DefaultCredentials;

                try
                {
                    byte[] data = wc.DownloadData(String.Format(@"https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", address, ConfigurationManager.AppSettings["GoogleApiKey"]));
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    string jsonData = UTF8Encoding.UTF8.GetString(data);

                    GoogleAddress googleComponent = JsonConvert.DeserializeObject<GoogleAddress>(jsonData);
                    if (googleComponent.status == "OK")
                    {
                        if (googleComponent.results.Count == 1) //Google mi da un risultato univoco allora sono sicuro che il cap sia quello giusto
                        {
                            ZipCode = (from x in googleComponent.results[0].address_components.AsQueryable()
                                       where x.types.Contains("postal_code")
                                       select x.long_name).FirstOrDefault();
                        }
                        else if (googleComponent.results.Count > 1)
                        { 
                        
                        }
                    }


                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                        {
                            // handle the 404 here
                        }
                    }
                    else if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                    {
                        //handle name resolution failure
                    }
                }
            }

            return ZipCode;
        }
    }

    #region Google Classes 
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }

    public class GoogleAddress
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
    #endregion


    [Serializable]
    public class GeocoderLocation
    {
        public string Status { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", Status, Latitude, Longitude);
        }
    }

    public class InformazioniPercorso
    {
        public GooglePath Path { get; set; }
        public string RawInfo { get; set; }
    }

    public class GooglePath
    {
        public List<string> destination_addresses;
        public List<string> origin_addresses;
        public List<PathElements> rows;
        public string status;
    }

    public class PathElements
    {
        public List<PathProperties> elements;
    }

    public class PathProperties
    {
        public PropertyPair distance;
        public PropertyPair duration;
        public string status;
    }

    public class PropertyPair
    {
        public string text { get; set; }
        public int value { get; set; }
    }
}