<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/FulcrumMaster.Master" CodeBehind="VeirzonFormUpdate.aspx.cs" Inherits="FulCrum.VeirzonFormUpdate" %>

<%@ Register Assembly="Telerik.Web.UI" TagPrefix="Telerik" Namespace="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
     <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js "></script>  
    <div class="wrapper">
        <div class="container-fluid">
            <div class="contentArea">
                <p class="pageHeading">VZ_MA_MRE</p>
                <div class="errorDivForms" id="tr_ErrorRow" runat="server" align="center">
                    <asp:Label CssClass="errorMessageForms" ID="lblError" runat="server" Visible="False" Height="15px" ForeColor="Red"></asp:Label>
                    <asp:Label CssClass="infoMessageForms" ID="lblInfo" runat="server" Visible="False" Height="15px" ForeColor="Green"></asp:Label>
                </div>

                <div class="row m-b-30" style="background-color: #fff; padding: 10px 5px; ">
                    <div class="w3-row-padding">
                        <div class="w3-row-padding padding-none">
                            <div class="w3-quarter">
                                <label>EWO
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="rfvComboewo" runat="server" Text="*" ErrorMessage="Please select ewo" ForeColor="Red" ControlToValidate="RadComboewo" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                <Telerik:RadComboBox RenderMode="Lightweight" Width="160px" MaxHeight="150" ID="RadComboewo" MarkFirstMatch="True" Filter="Contains" AllowCustomText="true" runat="server" OnSelectedIndexChanged="RadComboewo_SelectedIndexChanged" AutoPostBack="true" EmptyMessage="select ewo"></Telerik:RadComboBox>
                            </div>
                            <div class="w3-quarter"></div>
                            <div class="w3-quarter">
                                <label>&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                <asp:Button ID="btn_Submit" runat="server" Text="Import"  CssClass="btn btn-primary" OnClick="LnkExport_Click" />
                                <%--<asp:LinkButton ID="LnkExport" runat="server" Width="120px" Text="Export" ForeColor="Blue" OnClick="LnkExport_Click"></asp:LinkButton>--%>
                            </div>
                            <div class="w3-quarter">
                                <label>&nbsp;&nbsp;&nbsp;&nbsp;</label><br />
                                <%--<input type="button" ID="lnkDownload" runat="server" onclick="lnkDownload_Click" CausesValidation="false" ForeColor="Blue" Text="Download" />--%>
                                 <asp:Button ID="lnkDownload" CausesValidation="false" Enabled="false" runat="server" Text="Export to Excel"  CssClass="btn btn-primary" OnClick="lnkDownload_Click" />
                               </div>
                        </div>
                    </div> 
                     <%--<div class="w3-row-padding">
                         <div> <div class="w3-quarter">  <label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label><br /> </div>
                                <label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" runat="server" Text="*" ErrorMessage="Please select ewo" ForeColor="Red" ControlToValidate="RadComboewo" Display="Dynamic"></asp:RequiredFieldValidator></label><br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LinkButton1"  Text="Download All Images" Font-Size="Large" ForeColor="DarkBlue" OnClick="lnkDownloadAllImage_Click" runat="server"></asp:LinkButton>
                            </div>
                           
                         </div>--%>
                    <div id="map" style="width:100%; height:450px"></div>  
                </div>
            </div>
        </div> 
    </div>
         <script type="text/javascript">
 
  var markers = [
           <asp:Repeater ID="rptMarkers" runat="server">
               <ItemTemplate>
                       {
                             "ewo": '<%# Eval("ewo") %>',
                           "Equipment": '<%# Eval("POLE") %>',
                               "lng": '<%# Eval("LONGITUDE") %>',
                               "lat": '<%# Eval("LATITUDE") %>',                              
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
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                gestureHandling: 'greedy'
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
                        size: new google.maps.Size(50, 50),
                        origin: new google.maps.Point(0, 0),
                        anchor: new google.maps.Point(17, 34),
                        scaledSize: new google.maps.Size(20, 20)
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
                   icon: 'Images/pole_green.png',
                   map: map,
                   title: data.title,
                   url: data.Url
               });     

                 
                 (function (marker, data) {
                   google.maps.event.addListener(marker, "click", function (e) {

                       var PoleDetailsContent = "<table>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left' style='font-size:14px' colspan=3><b><u>Pole Information</u></b></td></tr>";
                        PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>EWO</b></td><td>:</td><td align='left'>" + data.ewo + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Pole Name</b></td><td>:</td><td align='left'>" + data.Equipment + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Latitude</b></td><td>:</td><td align='left'>" + data.lat + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Longitude</b></td><td>:</td><td align='left'>" + data.lng + "</td></tr>"; 
                       PoleDetailsContent = PoleDetailsContent + "</table>";

                       infoWindow.setContent(PoleDetailsContent);
                       infoWindow.open(map, marker);
                   });
               })(marker, data);

               (function (marker, data) {
                   google.maps.event.addListener(marker, "mouseover", function (e) {

                       var PoleDetailsContent = "<table>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left' style='font-size:14px' colspan=3><b><u>Pole Information</u></b></td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>EWO</b></td><td>:</td><td align='left'>" + data.ewo + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Pole Name</b></td><td>:</td><td align='left'>" + data.Equipment + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Latitude</b></td><td>:</td><td align='left'>" + data.lat + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Longitude</b></td><td>:</td><td align='left'>" + data.lng + "</td></tr>"; 
                       
                       PoleDetailsContent = PoleDetailsContent + "</table>";

                       infoWindow.setContent(PoleDetailsContent);

                       //infoWindow.setContent(data.description);
                       infoWindow.open(map, marker);
                   });
               })(marker, data);

               (function (marker, data) {
                   google.maps.event.addListener(marker, "mouseout", function (e) {

                       var PoleDetailsContent = "<table>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left' style='font-size:14px' colspan=3><b><u>Pole Information</u></b></td></tr>";
                        PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>EWO</b></td><td>:</td><td align='left'>" + data.ewo + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Pole Name</b></td><td>:</td><td align='left'>" + data.Equipment + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Latitude</b></td><td>:</td><td align='left'>" + data.lat + "</td></tr>";
                       PoleDetailsContent = PoleDetailsContent + "<tr><td align='left'><b>Longitude</b></td><td>:</td><td align='left'>" + data.lng + "</td></tr>"; 

                       PoleDetailsContent = PoleDetailsContent + "</table>";

                       infoWindow.setContent(PoleDetailsContent);
                       //infoWindow.setContent(data.description);
                       infoWindow.close(map, marker);
                   });
               })(marker, data);          
           }            
       }           
         </script>


    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAgTg6kJ4h8TC9uauosz5pJJlVbm1SjBpM&callback=initMap&libraries=places"
    async defer></script> 

      <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="float-right">
                        <div class="row" style="width: 500px;"> 
                            <div class="col-sm-6">
                                <div class="form-group row">
                                    <label for="example-text-input" class="col-sm-4 col-form-label">
                                      
                                    </label>
                                    <div class="col-sm-6">
                                        
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-6">
                                    <div class="form-group row">
                                        <asp:LinkButton ID="lnkDownloadAllImage" Text="Download All Images" Font-Size="Large" ForeColor="DarkBlue" OnClick="lnkDownloadAllImage_Click" runat="server"></asp:LinkButton>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                              </div> 
                        </div> 
                    </div>
                </div>
            </div>
        </div>

     

      <div class="w3-row-padding padding-none">
        <Telerik:RadGrid ID="rgImage" runat="server" MasterTableView-AllowFilteringByColumn="false" OnItemDataBound="rgImage_ItemDataBound" OnItemCommand="rgImage_ItemCommand" OnNeedDataSource="rgImage_NeedDataSource" AllowFilteringByColumn="true" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false"   PageSize="20" TableLayout="Fixed" Width="100%" Skin="Bootstrap" RenderMode="Lightweight" HeaderStyle-Font-Size="12px" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-Font-Bold="false" ItemStyle-HorizontalAlign="Left" >
            <GroupingSettings CaseSensitive="false" />
            <MasterTableView>
                <Columns>
                  <Telerik:GridBoundColumn DataField="Rename" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="Picture Rename" ItemStyle-HorizontalAlign="Left" UniqueName="Rename" Visible="true"></Telerik:GridBoundColumn>
                    <Telerik:GridBoundColumn DataField="pictureName" Display="false" FilterControlWidth="100px" HeaderStyle-Wrap="false" HeaderText="pictureName" ItemStyle-HorizontalAlign="Left" UniqueName="pictureName" Visible="true"></Telerik:GridBoundColumn>
                  
                    <telerik:GridHyperLinkColumn Target="_blank"
                        HeaderText="Photo"  UniqueName="PhotoAPI">  
                    </telerik:GridHyperLinkColumn>
                    <Telerik:GridTemplateColumn HeaderText="Download"  AllowFiltering="false" UniqueName="cmdLink" Visible="true">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="Link" ForeColor="Blue" CommandName="cmdLink" Text="Download" ></asp:LinkButton>
                        </ItemTemplate>
                    </Telerik:GridTemplateColumn>  
                </Columns>
            </MasterTableView>
        </Telerik:RadGrid>
    </div>
   
</asp:Content>
