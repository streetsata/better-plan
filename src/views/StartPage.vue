<template>
<div class="start_page-main_div">
<Navigation />
    <div id="carouselExampleControls" class="carousel slide" data-ride="carousel">
      <div class="carousel-inner">
        <div class="carousel-item active">
          <div class="cover-container-1">
            <div class="main-text">
              <h2>Управляй своими SMM проектами эффективно</h2>
              <p>
                Lorem ipsum dolor, sit amet consectetur adipisicing elit. Iusto provident saepe sunt
                repudiandae, modi laudantium cumque facere eaque animi ipsam sit velit blanditiis adipisci
                odit eum.
              </p>
              <div class="arrow-link">
                <a href>
                  Пробовать
                  <img src="images/red-right-arrow.svg" alt />
                </a>
              </div>
            </div>
            <div class="girl-img-1"></div>
          </div>
        </div>
        <div class="carousel-item">
          <div class="cover-container-2">
            <div class="girl-img-2"></div>
            <div class="main-text page-2-text">
              <h2>Делись своим проектом с командой в 1 клик</h2>
            </div>
          </div>
        </div>
        <div class="carousel-item">
          <div class="cover-container-3">
            <div class="main-text page-3-text">
              <h2>Редактируй контент в реальном времени</h2>
            </div>
            <div class="girl-img-3"></div>
          </div>
        </div>
        <div class="carousel-item">
          <div class="cover-container-4">
            <div class="main-text page-4-text">
              <h2>Давай делать идеальные проекты</h2>
            </div>
            <div class="start-work">
              <a href="#">
                Начать
                <img src="../assets/img/Arrow-right.png" alt="Arrow for beginning" />
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
    <a
      class="carousel-control-prev"
      href="#carouselExampleControls"
      role="button"
      data-slide="prev"
    >
      <span class="carousel-control-prev-icon" aria-hidden="true"></span>
      <span class="sr-only">Previous</span>
    </a>
    <a
      class="carousel-control-next"
      href="#carouselExampleControls"
      role="button"
      data-slide="next"
    >
      <span class="carousel-control-next-icon" aria-hidden="true"></span>
      <span class="sr-only">Next</span>
    </a>
</div>
</template>

<script>
import Navigation from "../components/Navigation";

export default {
 components: {Navigation}
};
// src = "https://code.jquery.com/jquery-3.4.1.slim.min.js";
// integrity =
//   "sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n";
// crossorigin = "anonymous";
// src = "https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js";
// integrity =
//   "sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo";
// crossorigin = "anonymous";
// src = "https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js";
// integrity =
//   "sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6";
// crossorigin = "anonymous";
</script>

<style scoped>
* {
    background: #f8f8f8;
}

.start_page-main_div {
  height: 100%;
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
    background-image: url(../assets/img/girl-image-1.jpg);
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

.arrow-link>a {
    color: #F4896F;
}
.arrow-link>a:hover {
    text-decoration: none;
}
.cover-container-2 {
    display: flex;
    flex-direction: row;
    height: 90vh;   
}

.girl-img-2 {
    width: 50%;
    background-image: url(../assets/img/girl-image-2.png);
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
.girl-img-3{
    height: 80%;
    width: 80%;
    align-self: center;
    background-image: url(../assets/img/girl-image-3.png);
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
    background: url(../assets/img/Heart-like.png) no-repeat 100% 0%;
    padding-top: 10%;
    padding-bottom: 5%;
    
}
.start-work {
    display: flex;
    justify-content: center;
}

.start-work>a {
color: #2C473E;
}
.start-work>a:hover {
    text-decoration: none;
}
</style>