<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminDashBoard.aspx.cs" Inherits="Chow_Hub_FrontEnd.AdminDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta name="keywords" content="tailwind,tailwindcss,tailwind css,css,starter template,free template,admin templates, admin template, admin dashboard, free tailwind templates, tailwind example">
    <!-- Css -->
    <link rel="stylesheet" href="./dist/styles.css">
    <link rel="stylesheet" href="./dist/all.css">
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:400,400i,600,600i,700,700i" rel="stylesheet">
    <title>Dashboard | Tailwind Admin</title>
    <style>
        .body
        {
            margin-bottom:150px;
            margin-top:150px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Container -->
<div class="mx-auto bg-grey-400">
    <!--Screen-->
    <div class="min-h-screen flex flex-col">
        <!--Header Section Starts Here-->
        <header class="bg-nav">
            <div class="flex justify-between">
                <div class="p-1 mx-3 inline-flex items-center">
                    <i class="fas fa-bars pr-2 text-white" onclick="sidebarToggle()"></i>
                    <h1 class="text-white p-2">Logo</h1>
                </div>
                <div class="p-1 flex flex-row items-center">
                    <a href="https://github.com/tailwindadmin/admin" class="text-white p-2 mr-2 no-underline hidden md:block lg:block">Github</a>


                    <img onclick="profileToggle()" class="inline-block h-8 w-8 rounded-full" src="https://avatars0.githubusercontent.com/u/4323180?s=460&v=4" alt="">
                    <a href="#" onclick="profileToggle()" class="text-white p-2 no-underline hidden md:block lg:block">Adam Wathan</a>
                    <div id="ProfileDropDown" class="rounded hidden shadow-md bg-white absolute pin-t mt-12 mr-1 pin-r">
                        <ul class="list-reset">
                          <li><a href="#" class="no-underline px-4 py-2 block text-black hover:bg-grey-light">My account</a></li>
                          <li><a href="#" class="no-underline px-4 py-2 block text-black hover:bg-grey-light">Notifications</a></li>
                          <li><hr class="border-t mx-2 border-grey-ligght"></li>
                          <li><a href="#" class="no-underline px-4 py-2 block text-black hover:bg-grey-light">Logout</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </header>
        <!--/Header-->

        <div class="flex flex-1">
            <!--Sidebar-->
            <aside id="sidebar" class="bg-side-nav w-1/2 md:w-1/6 lg:w-1/6 border-r border-side-nav hidden md:block lg:block">

                <ul class="list-reset flex flex-col">
                    <li class=" w-full h-full py-3 px-2 border-b border-light-border bg-white">
                        <a href="AdminDashBoard.html"
                           class="font-sans font-hairline hover:font-normal text-sm text-nav-item no-underline">
                            <i class="fas fa-tachometer-alt float-left mx-2"></i>
                            Dashboard
                            <span><i class="fas fa-angle-right float-right"></i></span>
                        </a>
                    </li>       
                    <li class="w-full h-full py-3 px-2">                       
                        <ul class="list-reset -mx-2 bg-white-medium-dark">
                            <li class="border-t mt-2 border-light-border w-full h-full px-2 py-3">
                                <a href="login.aspx"
                                   class="mx-4 font-sans font-hairline hover:font-normal text-sm text-nav-item no-underline">
                                    Login Page
                                    <span><i class="fa fa-angle-right float-right"></i></span>
                                </a>
                            </li>
                            <li class="border-t border-light-border w-full h-full px-2 py-3">
                                <a href="register.aspx"
                                   class="mx-4 font-sans font-hairline hover:font-normal text-sm text-nav-item no-underline">
                                    Register Page
                                    <span><i class="fa fa-angle-right float-right"></i></span>
                                </a>
                            </li>
                          
                        </ul>
                    </li>
                </ul>

            </aside>
            <!--/Sidebar-->
            <!--Main-->
            <main class="bg-white-300 flex-1 p-3 overflow-hidden">

				<asp:Literal runat="server" ID="content" />

                    <!-- /Stats Row Ends Here -->

                    <!-- Card Sextion Starts Here -->
                     <div class="flex flex-1 flex-col md:flex-row lg:flex-row mx-2">

                        <!-- card -->

                        <div class="rounded overflow-hidden shadow bg-white mx-2 w-full">
                            <div class="px-6 py-2 border-b border-light-grey">
                                <div class="font-bold text-xl">Trending Categories</div>
                            </div>
                            <div class="table-responsive">
                                <table class="table text-grey-darkest">
                                    <thead class="bg-grey-dark text-white text-normal">
                                    <tr>
                                        <th scope="col">Category ID</th>
                                        <th scope="col">Category</th>
                                        <th scope="col">Total Sales</th>
                                        <th scope="col">Number of Purchases</th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    <tr>
                                        <th scope="row">1</th>
                                        <td>
                                            <button class="bg-blue-500 hover:bg-blue-800 text-white font-light py-1 px-2 rounded-full">
												BreakFast
                                            </button>
                                        </td>
                                        <td>4500</td>
                                        <td>4600</td>
                                        
                                    </tr>
                                    <tr>
                                        <th scope="row">2</th>
                                        <td>
                                            <button class="bg-primary hover:bg-primary-dark text-white font-light py-1 px-2 rounded-full">
                                               Lunch
                                            </button>
                                        </td>
                                        <td>10000</td>
                                        <td>3000</td>
                                        
                                    </tr>

                                    <tr>
                                        <th scope="row">3</th>
                                        <td>
                                            <button class="bg-success hover:bg-success-dark text-white font-light py-1 px-2 rounded-full">
                                                Dinner
                                            </button>
                                        </td>
                                        <td>10000</td>
                                        <td>3000</td>
                                        
                                    </tr>

                                    <tr>
                                        <th scope="row">4</th>
                                        <td>
                                            <button class="bg-blue-500 hover:bg-blue-800 text-white font-light py-1 px-2 rounded-full">
                                                Drinks
                                            </button>
                                        </td>
                                        <td>10000</td>
                                        <td>3000</td>
                                       
                                    </tr>

                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /card -->

                    </div>
                    <!-- /Cards Section Ends Here -->

                    <!-- Progress Bar -->
                    <div class="flex flex-1 flex-col md:flex-row lg:flex-row mx-2 mt-2">
                        <div class="rounded overflow-hidden shadow bg-white mx-2 w-full pt-2">
                            <div class="px-6 py-2 border-b border-light-grey">
                                <div class="font-bold text-xl">Popular Categories By Rank</div>
                            </div>
                            <div class="">
                                <div class="w-full">

                                    <div class="shadow w-full bg-grey-light">
                                        <div class="bg-blue-500 text-xs leading-none py-1 text-center text-white"
                                             style="width: 45%">45%
                                        </div>
                                    </div>


                                    <div class="shadow w-full bg-grey-light mt-2">
                                        <div class="bg-teal-500 text-xs leading-none py-1 text-center text-white"
                                             style="width: 55%">55%
                                        </div>
                                    </div>


                                    <div class="shadow w-full bg-grey-light mt-2">
                                        <div class="bg-orange-500 text-xs leading-none py-1 text-center text-white"
                                             style="width: 65%">65%
                                        </div>
                                    </div>


                                    <div class="shadow w-full bg-grey-300 mt-2">
                                        <div class="bg-red-800 text-xs leading-none py-1 text-center text-white"
                                             style="width: 75%">75%
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Profile Tabs-->
                    <div class="flex flex-1 flex-col md:flex-row lg:flex-row mx-2 p-1 mt-2 mx-auto lg:mx-2 md:mx-2 justify-between">
                        <!--Top user 1-->
                        <div class="rounded rounded-t-lg overflow-hidden shadow max-w-xs my-3">
                            <img src="https://i.imgur.com/w1Bdydo.jpg" alt="" class="w-full"/>
                            <div class="flex justify-center -mt-8">
                                <img src="https://i.imgur.com/8Km9tLL.jpg" alt=""
                                     class="rounded-full border-solid border-white border-2 -mt-3">
                            </div>
                            <div class="text-center px-3 pb-6 pt-2">
                                <h3 class="text-black text-sm bold font-sans">Olivia Dunham</h3>
                                <p class="mt-2 font-sans font-light text-grey-700">Hello, i'm from another the other
                                    side!</p>
                            </div>
                            <div class="flex justify-center pb-3 text-grey-dark">
                                <div class="text-center mr-3 border-r pr-3">
                                    <h2>34</h2>
                                    <span>Photos</span>
                                </div>
                                <div class="text-center">
                                    <h2>42</h2>
                                    <span>Friends</span>
                                </div>
                            </div>
                        </div>
                        <!--Top user 2-->
                        <div class="rounded rounded-t-lg overflow-hidden shadow max-w-xs my-3">
                            <img src="https://i.imgur.com/w1Bdydo.jpg" alt="" class="w-full"/>
                            <div class="flex justify-center -mt-8">
                                <img src="https://i.imgur.com/8Km9tLL.jpg" alt=""
                                     class="rounded-full border-solid border-white border-2 -mt-3">
                            </div>
                            <div class="text-center px-3 pb-6 pt-2">
                                <h3 class="text-black text-sm bold font-sans">Olivia Dunham</h3>
                                <p class="mt-2 font-sans font-light text-grey-dark">Hello, i'm from another the other
                                    side!</p>
                            </div>
                            <div class="flex justify-center pb-3 text-grey-dark">
                                <div class="text-center mr-3 border-r pr-3">
                                    <h2>34</h2>
                                    <span>Photos</span>
                                </div>
                                <div class="text-center">
                                    <h2>42</h2>
                                    <span>Friends</span>
                                </div>
                            </div>
                        </div>
                        <!--Top user 3-->
                        <div class="rounded rounded-t-lg overflow-hidden shadow max-w-xs my-3">
                            <img src="https://i.imgur.com/w1Bdydo.jpg" alt="" class="w-full"/>
                            <div class="flex justify-center -mt-8">
                                <img src="https://i.imgur.com/8Km9tLL.jpg" alt=""
                                     class="rounded-full border-solid border-white border-2 -mt-3">
                            </div>
                            <div class="text-center px-3 pb-6 pt-2">
                                <h3 class="text-black text-sm bold font-sans">Olivia Dunham</h3>
                                <p class="mt-2 font-sans font-light text-grey-dark">Hello, i'm from another the other
                                    side!</p>
                            </div>
                            <div class="flex justify-center pb-3 text-grey-dark">
                                <div class="text-center mr-3 border-r pr-3">
                                    <h2>34</h2>
                                    <span>Photos</span>
                                </div>
                                <div class="text-center">
                                    <h2>42</h2>
                                    <span>Friends</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/Profile Tabs-->
                </div>
            </main>
        <!--Footer-->
        <footer class="bg-grey-darkest text-white p-2">
            <div class="flex flex-1 mx-auto">&copy; My Design</div>
            <div class="flex flex-1 mx-auto">Distributed by:  <a href="https://themewagon.com/" target=" _blank">Themewagon</a></div>
        </footer>
        <!--/footer-->

    </div>

</div>
<script src="./main.js"></script>
</asp:Content>
