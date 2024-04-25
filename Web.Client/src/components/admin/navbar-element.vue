<script>
export default {
  name: "navbar-element"
}
</script>

<template>
  <div class="nav-bar">
    <div class="title">
      Админ панель
    </div>
    <div class="item" @click="this.$router.push('/catalog-panel')">
      <div>Каталоги</div>
    </div>
    <div class="item" @click="this.$router.push('/product-panel')">
      <div>Продукты</div>
    </div>
    <div class="item" @click="this.$router.push('/parameter-panel')">
      <div>Параметры</div>
    </div>
    <div class="item" @click="this.$router.push('/user-panel')">
      <div>Пользователи</div>
    </div>
    <div class="item" @click="this.$router.push('/slider-panel')">
      <div>Слайдер</div>
    </div>
    <div class="item" @click="this.$router.push('/banner-panel')">
      <div>Баннер</div>
    </div>
  </div>
</template>

<style scoped>
.nav-bar {
  background: #373f50;
  width: 15%;
  height: 100vh;
  position: sticky;
  top: 0;
  left: 0;
}

.title {
  width: 100%;
  text-align: center;
  color: #fff;
  font-size: 24px;
  margin-bottom: 50px;
  padding-top: 30px;
}

.item {
  width: 100%;
  height: 45px;
  font-size: 18px;
  display: flex;
  align-items: center;
  padding: 0 20px;
  color: #fff;
  text-decoration: none;
}

.item:hover {
  background-color: #474E5E;
}
</style>