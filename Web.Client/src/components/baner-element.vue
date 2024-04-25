<script>
export default {
  name: "baner-element",
  props: {
    banner: {}
  }
}
</script>

<template>
  <div class="baner" v-if="banner">
    <img :src="'http://' + banner.imagePath">
    <div class="card-info">
      <div class="title">
        {{ banner.title }}
      </div>
      <div class="text">
        {{ banner.text }}
      </div>
      <button class="btn" @click="this.$router.push(banner.targetProductPath)">Подрбнее</button>
    </div>
  </div>
</template>

<style scoped>
.baner {
  width: 100%;
  height: 450px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  position: relative;
  align-items: end;

}

img {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: -1;
  border-radius: 20px;
}

.card-info {
  width: 300px;
  height: 300px;
  background-color: #373f50A6;
  margin-right: 50px;
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 30px;
  color: #fff;
}

.title {
  font-size: 24px;
  margin-bottom: 15px;
}

.text {
  font-size: 14px;
  margin-bottom: 25px;
}

.btn {
  width: 150px;
  height: 35px;
  color: #fff;
  background-color: #fe696a;
}
</style>