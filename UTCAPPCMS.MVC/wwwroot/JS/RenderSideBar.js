$(document).ready(function () {

    var sidebar = `
               <div class="layout-sidebar-backdrop"></div>
        <div class="layout-sidebar-body">
            <div class="custom-scrollbar">
                <nav id="sidenav" class="sidenav-collapse collapse">
                    <ul class="sidenav level-1">
                        <li class="sidenav-search">
                            <form class="sidenav-form" action="/">
                                <div class="form-group form-group-sm">
                                    <div class="input-with-icon">
                                        <input class="form-control" type="text" placeholder="Search…">
                                        <span class="icon icon-search input-icon"></span>
                                    </div>
                                </div>
                            </form>
                        </li>

                        <li class="sidenav-heading">Navigation</li>
                        <li class="sidenav-item has-subnav">
                            <a href="dashboard-1.html" aria-haspopup="true">
                                <span class="sidenav-icon icon icon-works">&#103;</span>
                                <span class="sidenav-label">Dashboards</span>
                            </a>
                            <ul class="sidenav level-2 collapse">
                                <li class="sidenav-heading">DataTables</li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="Parkings" asp-action="Index">Parking</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="ParkingImges" asp-action="Index">ParkingImges</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="ParkingLocations" asp-action="Index">ParkingLocations</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="SiteLine" asp-action="Index">SiteLine</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="Subscription" asp-action="Index">Subscription</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="TransactionDetails" asp-action="Index">TransactionDetails</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="CollectionTransaction" asp-action="Index">CollectionTransaction</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="PaymentSource" asp-action="Index">PaymentSource</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="TransactionStatus" asp-action="Index">TransactionStatus</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="Tariff" asp-action="Index">Tariff</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="AdminUsers" asp-action="Index">AdminUsers</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="AuditTrialid" asp-action="Index">AuditTrialid</a></li>
                                <li><a class="nav-link text-dark" asp-area="" asp-controller="UserType" asp-action="Index">UserType</a></li>
                            </ul>
                        </li>

                    </ul>
                </nav>
            </div>
        </div>
    `;
    $('#RenderBar').html(sidebar);
   
});