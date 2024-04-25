<script>
export default {
  name: "header-element"
}
</script>

<template>
  <header>
    <div class="top">
      <div class="container">
        <div class="top-item support">
          <a href="tel:79779282887">
            Поддержка клиентов +7 (977) 928-28-87
          </a>
        </div>
        <div class="top-item text-info">Продажа мягкой мебели с доставкой</div>
      </div>
    </div>
    <div class="mid">
      <div class="logo" @click="this.$router.push('/main')">
        <img class="logotype" src="/icons/logo.svg"/>
      </div>

      <div class="bottom catalog">
        <div class="catalog-item" @click="this.$router.push('/catalog')">
          Каталог
        </div>
        <div class="catalog-item" @click="this.$router.push('/about')">
          О нас
        </div>
        <div class="catalog-item" @click="this.$router.push('/main')">
          <a href="#callback">Обратная связь</a>
        </div>
      </div>
    </div>

  </header>
</template>

<style scoped>
.logotype {
  width: 60px;
  height: 60px;
}

header {
  background-color: #373f50;
  color: #ffffffa6;
}

.top {
  margin: auto;
  background-color: #2b3445;
}

.container {
  width: 100%;
  display: flex;
  align-items: center;
  padding: 10px 0;
  font-size: 14px;
  max-width: 1280px;
  margin: 0 auto;
}

.top-item {
  margin-right: 50px;
  cursor: pointer;
}

.top-item:hover {
  color: #fbffcc;
}


.mid {
  max-width: 1280px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
  color: #fff;
}

.logo {
  cursor: pointer;
}

.logotype {
  width: 60px;
  height: auto;
}

.bottom {
  display: flex;
  gap: 20px;
}

.catalog-item {
  cursor: pointer;
  font-size: 16px;
  font-weight: bold;
  color: #fff;
  transition: color 0.3s ease;
}

.catalog-item:hover {
  color: #fff;
  text-decoration: underline;
}

.catalog-item a {
  text-decoration: none;
  color: #fff;
}

.catalog-item a:hover {
  color: #fff;
}

a {
  text-decoration: none;
  color: #ffffffa6;
}
</style>