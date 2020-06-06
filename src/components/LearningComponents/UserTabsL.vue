<template>
   <div class="draft-tab">
    <input type="checkbox" id="tab1">
    <label for="tab1" class="tab-title"></label>
    <a href="#" class="create-project"><img src="../../assets/img/create_project.png" alt="create project"></a>

    <div class="draft__links-wrapper">
        <a class="project__name project__first" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
    </div>

    <div class="tab-content">
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
        <a class="project__name" href="#">Masterskaya @2.58 af</a>
    </div>
  </div>
</template>

<style scoped>
.draft-tab input, .tab-content {
  display: none;
}

.draft-tab {
  position: relative;
  width: 100%;
  max-width: 1733px;
  margin: 0 auto;
  padding: 6px 10px 6px 44px;
  background: #FFFFFF;
  border-radius: 30px;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: justify;
      -ms-flex-pack: justify;
          justify-content: space-between;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
  -ms-flex-wrap: wrap;
      flex-wrap: wrap;
}

.tab-title {
  position: absolute;
  right: 65px;
  width: 0;
  height: 0;
  border-left: 10px solid #FFFFFF;
  border-right: 10px solid #FFFFFF;
  border-top: 15px solid #B7CDC1;
  border-bottom: none;
  cursor: pointer;
}

.create-project {
  position: absolute;
  right: 6px;
  text-decoration: none;
  opacity: 0.7;
  width: 42px;
  height: 42px;
}

.draft__links-wrapper {
  width: 95%;
  display: -ms-grid;
  display: grid;
  -ms-grid-columns: (200px)[auto-fit];
      grid-template-columns: repeat(auto-fit, 200px);
  -ms-grid-rows: 42px;
      grid-template-rows: 42px;
}

.create-project:hover {
  opacity: 1;
}

.project__name {
  position: relative;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-pack: center;
      -ms-flex-pack: center;
          justify-content: center;
  -webkit-box-align: center;
      -ms-flex-align: center;
          align-items: center;
  min-width: 200px;
  min-height: 42px;
  margin-right: 1px;
  color: #86A296;
  font-weight: 400;
  font-size: 16px;
  line-height: 33px;
  text-decoration: none;
  border: 1px solid transparent;
  border-radius: 80px;
}

.project__name:after {
  position: absolute;
  right: 8px;
  content: "";
  width: 6px;
  height: 6px;
  background-color: #F39C86;
  border-radius: 50%;
  opacity: 0;
}

.project__name:hover, .project__name:focus {
  border-color: #86A296;
  font-weight: 700;
}

.project__name:focus::after {
  opacity: 1;
}

.draft-tab :checked ~ .tab-content {
  width: 95%;
  display: -ms-grid;
  display: grid;
  -ms-grid-columns: (200px)[auto-fit];
      grid-template-columns: repeat(auto-fit, 200px);
  -ms-grid-rows: 42px;
      grid-template-rows: 42px;
}

.checkbox {
  position: absolute;
  z-index: -1;
  opacity: 0;
  margin: 65% 0 0 0;
}

.checkbox-in-learning {
  margin: 0;
}

.checkbox + label {
  position: relative;
  padding: 0 0 0 26px;
  cursor: pointer;
}

.checkbox + label:before {
  content: '';
  position: absolute;
  top: -3px;
  left: 0;
  width: 20px;
  height: 20px;
  border: 1px solid #86A296;
  border-radius: 6px;
}

.checkbox + label:hover:before {
  background-color: #dfdfdf;
}

.checkbox:checked + label:before {
  background: #dfdfdf;
}

/* Create the checkmark/indicator (hidden when not checked) */
.checkbox + label:after {
  content: "";
  position: absolute;
  display: none;
  left: 7px;
  top: -2px;
  width: 7px;
  height: 12px;
  border: solid #2C473E;
  border-width: 0 3px 3px 0;
  -webkit-transform: rotate(45deg);
  transform: rotate(45deg);
}

/* Show the checkmark when checked */
.checkbox:checked + label:after {
  display: block;
}
</style>