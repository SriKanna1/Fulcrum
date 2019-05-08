<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FulcrumMaster.Master" CodeBehind="WebForm1.aspx.cs" Inherits="FulCrum.WebForm1" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        function DeleteRecord() {
            alert(Mytest);
            $.ajax({ 
                type: "DELETE",
                url: "https://api.fulcrumapp.com/api/v2/records/c7492ace-a071-4db2-a5eb-d8f2dd9b28e7.json?token=9e7a63ab3ad0906cfca9077c652ffa28a64c123571cd32444434a24a01c55af3f44ad0983d223cfb",
                contentType: "application/json",
                dataType: "json",
                headers: {
                    "X-ApiToken": "my-api-key"
                },
                success: function (data) {
                    // do something!
                    console.log(data);
                }
            });
        }
    </script>
  
    <label>First Name </label> 
    <br />
  <%--  <asp:TextBox ID="txtSync" CssClass="form-control" Text="Delete" runat="server"></asp:TextBox>--%>
       <asp:Button ID="btn_Submit" runat="server" Text="Import"  CssClass="btn btn-primary" OnClick="btn_Submit_Click"/>
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js "></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAgTg6kJ4h8TC9uauosz5pJJlVbm1SjBpM&callback=initMap&libraries=places"></script>
 
    <div id="map_canvas" style="width: 800px; height: 600px">
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            var latlng = new google.maps.LatLng(42.252235074978, -71.0015752539039);
            var myOptions = {
                zoom: 8,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        }); 
    </script>
</asp:Content>--%>

    <%--<html>
  <head>
    <style>
      /* Set the size of the div element that contains the map */
      #map {
        height: 400px;  /* The height is 400 pixels */
        width: 100%;  /* The width is the width of the web page */
       }
    </style>
  
<body>
    <h3>My Google Maps Demo</h3>
    <!--The div element for the map -->
    <div id="map"></div>
    <script>
// Initialize and add the map
function initMap() {
  // The location of Uluru
  var uluru = {lat: 42.252235074978, lng: -71.0015752539039};
  // The map, centered at Uluru
  var map = new google.maps.Map(
      document.getElementById('map'), {zoom: 4, center: uluru});
  // The marker, positioned at Uluru
  var marker = new google.maps.Marker({position: uluru, map: map});
}
    </script>
    <script async defer
    src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAgTg6kJ4h8TC9uauosz5pJJlVbm1SjBpM&callback=initMap&libraries=places">
    </script>

     
    <form id="form1" runat="server">
        <Telerik:RadScriptManager ID="RadScriptManager1" runat="server"></Telerik:RadScriptManager>
        <div>
             <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="false" ForeColor="Blue" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                        
                                       
            <asp:Button Id="dd" runat="server" />
        </div>
    </form>
</body>
      </head>
    
</html>--%>

    <%-- <script type="text/javascript">
 
  var markers = [
           <asp:Repeater ID="rptMarkers" runat="server">
               <ItemTemplate>
                       {
                           "Equipment": '1',
                               "lng": '-71.0013596713543',
                               "lat": '42.2523636262195',                             
                       }
                </ItemTemplate>
                <SeparatorTemplate>
                    ,
                </SeparatorTemplate>
            </asp:Repeater>
       ];
    </script>  

    <script>
        // This example requires the Places library. Include the libraries=places
        // parameter when you first load the API. For example:
        // <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">

        function initMap() {            
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };   
            var infoWindow = new google.maps.InfoWindow();  
            var map = new google.maps.Map(document.getElementById("map"), mapOptions);

            var search = document.getElementById('autocomplete');            
            searchBox = new google.maps.places.SearchBox(search);
            map.controls[google.maps.ControlPosition.LEFT_TOP].push(search);

            var markersC = [];

            map.addListener('bounds_changed', function() {
                searchBox.setBounds(map.getBounds());               
            }); 
        
            searchBox.addListener('places_changed', function() {                
                var places = searchBox.getPlaces();               
                if (places.length == 0) {
                    return;
                }  

                markersC.forEach(function(markerD) {
                    markerD.setMap(null);
                });
                markersC = [];

                var bounds = new google.maps.LatLngBounds();
                places.forEach(function(place) {
                    if (!place.geometry) {
                        console.log("Returned place contains no geometry");
                        return;
                    }
                    var icon = {
                        url: place.icon,
                        size: new google.maps.Size(71, 71),
                        origin: new google.maps.Point(0, 0),
                        anchor: new google.maps.Point(17, 34),
                        scaledSize: new google.maps.Size(25, 25)
                    };

                    // Create a marker for each place.
                    markersC.push(new google.maps.Marker({
                        map: map,
                        icon: icon,
                        title: place.name,
                        position: place.geometry.location
                    }));

                    if (place.geometry.viewport) {                       
                        bounds.union(place.geometry.viewport);
                    } else {
                        bounds.extend(place.geometry.location);
                    }
                });
                map.fitBounds(bounds);
            });   

            var directionsService = new google.maps.DirectionsService;
            var directionsDisplay = new google.maps.DirectionsRenderer;            
            directionsDisplay.setMap(map);     
             

           for (i = 0; i < markers.length; i++) {         
               var data = markers[i];           
               var myLatlng = new google.maps.LatLng(data.lat, data.lng);
               var marker = new google.maps.Marker({
                   position: myLatlng,
                   icon: '',
                   map: map,
                   title: data.title,
                   url: data.Url
               });     
           }            
       }           
    </script>

    <div id="map" style="width:100%; height:400px"></div>  
      <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAgTg6kJ4h8TC9uauosz5pJJlVbm1SjBpM&callback=initMap&libraries=places"
    async defer></script>--%>
</asp:Content>
