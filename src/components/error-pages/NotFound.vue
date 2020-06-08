<template>
    <div>
        <header>
            <b-nav class="navbar navbar-expand-lg navbar-light">
                <a class="navbar-brand" href="#">
                    <img src="../../assets/img/Group 78.svg" alt="BetterPlan Logo">
                </a>
                <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNavDropdown"
                        aria-controls="navbarNavDropdown" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNavDropdown">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link other" href="#">возможности</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link other" href="#">обучение</a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link other" href="#">помощь</a>
                        </li>
                        <li class="nav-item">
                            <a class="b-nav-link enter" href="#">войти</a>
                        </li>
                    </ul>
                </div>
                <nav class="navbar navbar-light">
                    <a class="navbar-brand" href="#">
                        <img src="../../assets/img/FB-logo.svg" alt="FB-logo">
                    </a>
                    <a class="navbar-brand" href="#">
                        <img src="../../assets/img/insta-logo.svg" alt="Instagram">
                    </a>
                </nav>
            </b-nav>
        </header>

        <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
            <div class="carousel-inner">
                <div class="carousel-item active">
                    <div class="cover-container-1">
                        <div class="main-text text404">
                            <h2>Ошибка 404</h2>
                            <p>Упс, тут ничего, давайте начнем с главной</p>
                            <div class="arrow-link link404">
                                <a href="/">На главную <img src="../../assets/img/red-right-arrow.svg" alt=""></a>
                            </div>
                        </div>
                        <div class="girl-img-1 error404">
                            <img class="image404" src="../../assets/img/404.png" alt="pink girl">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "NotFound"
    }
</script>

<style scoped>
    * {
        background: #f8f8f8;
    }

    .other {
        text-decoration: none;
        font-family: 'Lato', sans-serif;
        font-style: normal;
        font-weight: 300;
        font-size: 14px;
        line-height: 20px;
        color: #2C473E !important;
        text-transform: capitalize;
        text-align: center;
        border-width: 10px;
        padding: 4px 14px;
    }

    .other::before {
        margin-right: 10px;
        content: '[';
        color: #2C473E;
        -webkit-transform: translateX(20px);
        -moz-transform: translateX(20px);
        transform: translateX(20px);
    }

    .other::after {
        margin-left: 10px;
        content: ']';
        color: #2C473E;
        transform: translateX(-20px);
        -webkit-transform: translateX(20px);
        -moz-transform: translateX(20px);
    }

    .other::before, .other::after {
        opacity: 0;
        -webkit-transition: -webkit-transform 0.3s, opacity 0.2s;
        -moz-transition: -moz-transform 0.3s, opacity 0.2s;
        transition: transform 0.3s, opacity 0.2s;
    }

    .other:hover::before, .other:hover::after {
        opacity: 1;
        -webkit-transform: translateX(0px);
        -moz-transform: translateX(0px);
        transform: translateX(0px);
    }

    .enter {
        text-decoration: none;
        color: #F4896F !important;
        font-family: 'Lato', sans-serif;
        font-style: normal;
        font-weight: 300;
        font-size: 14px;
        line-height: 20px;
        text-transform: capitalize;
        text-align: center;
        border-width: 10px;
        padding: 4px 14px;
    }

    .enter:hover {
        text-decoration: none;
        background-color: #F4896F !important;
        color: #fff !important;
        border-radius: 14px;
    }

    .carousel-inner {
        height: 90vh;
    }

    .cover-container-1 {
        display: flex;
        flex-direction: row;
        height: 90vh;

    }

    .main-text {
        width: 50%;
        padding: 5%;
        align-self: center;
    }

    .text404 {
        padding-left: 20%;
    }

    .girl-img-1 {
        width: 50%;
        background-image: url(../../assets/img/girl-image-1.jpg);
        background-repeat: no-repeat;
        background-size: 100%;
        background-position: 100% 56%;
    }

    .error404 {
        background-image: none;
        margin: auto;
    }

    .image404 {
        width: 100%;
        max-width: 490px;
        height: auto;
        max-height: 490px;
    }

    .arrow-link {
        display: flex;
        flex-direction: row;
        justify-content: flex-end;
        padding-right: 10px;
    }

    .link404 {
        margin-top: 20%;
        padding-right: 20%;
    }

    .arrow-link > a {
        color: #F4896F;
    }

    .arrow-link > a:hover {
        text-decoration: none;
    }

    .cover-container-2 {
        display: flex;
        flex-direction: row;
        height: 90vh;
    }

    .girl-img-2 {
        width: 50%;
        background-image: url(../../assets/img/girl-image-2.png);
        background-repeat: no-repeat;
        background-size: 100%;
        background-position: 0% 51%;
    }

    .cover-container-3 {
        display: flex;
        flex-direction: column;
        height: 90vh;
    }

    .page-3-text {
        width: auto;
        align-self: center;
        padding-bottom: 0;
    }

    .girl-img-3 {
        height: 80%;
        width: 80%;
        align-self: center;
        background-image: url(../../assets/img/girl-image-3.png);
        background-repeat: no-repeat;
        background-size: contain;
        background-position: 50% 50%;
    }

    .cover-container-4 {
        display: flex;
        flex-direction: column;
        justify-content: center;
        height: 90vh;
    }

    .page-4-text {
        color: #F4896F;
        width: auto;
        background: url(../../assets/img/_Heart-like.png) no-repeat 100% 0;
        padding-top: 10%;
        padding-bottom: 5%;

    }

    .start-work {
        display: flex;
        justify-content: center;
    }

    .start-work > a {
        color: #2C473E;
    }

    .start-work > a:hover {
        text-decoration: none;
    }

</style>