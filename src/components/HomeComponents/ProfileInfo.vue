<template>
    <div class="profile-container">
        <div class="wrapper">
            <img class="header-img" src="/images/header.png" alt="background">
            <div class="user-info">
                <img src="/images/avatar.png" alt="avatar">
                <span>Natalie Chelovek</span>
            </div>
            <div class="profile-info">
                <div class="short-info">
                    <div class="info-header">
                        <img src="/images/earth.svg" alt="earth">
                        <span>Краткая информация</span>
                    </div>
                    <div class="about-me">
                        Счастливая мамочка<br>люблю себя блаблалалала
                    </div>
                    
                </div>
                <div class="photos">
                    <div class="info-header">
                        <img src="/images/image.svg" alt="earth">
                        <span>Фото</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.profile-container{
    /* padding-left: 5em; */
    /* padding-right: 5em; */
    /* width: 50%; */
    height: 100%;
    /* background: grey; */
    display: flex;
    justify-content: center;
    margin-left: 90px;
}

.header-img{
    width: 46.25rem;
    height: 17.1875rem;
    border-radius: 4px;
}
.user-info{
    position: relative;
    bottom: 85px;
    display: flex;
    flex-direction: row;
    align-items: center;
    margin-left: 16px;
}
.user-info img{
    border: solid white 3px;
    border-radius: 50%;
}
.user-info span{
    font-family: Lato;
    font-style: normal;
    font-weight: bold;
    font-size: 24px;
    line-height: 34px;
    color: white;
    margin-left: 19px;
}
.profile-info{
    display: flex;
    justify-content: space-between;
    
    margin-top: -75px;
}
.profile-info .short-info{
    background-color: white;
    width: 354px;
    height: 422px;
    border-radius: 4px;
    
}
.short-info .about-me{
    margin-top: 5px;
    font-size: 14px;
    line-height: 20px;  
    text-align: center;
    font-weight: 600;
}
.profile-info .photos{
    background-color: white;
    width: 354px;
    height: 422px;
    border-radius: 4px;
    
}

.info-header{
    display: flex;
    margin: 8px 12px;
}

.info-header span{
    font-family: sans-serif;
    font-size: 16px;
    color: #2B2B2B;
    margin-left: 8px;
    margin-top: 6px;
}
</style>