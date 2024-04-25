<script>
export default {
  name: "product-card",
  props: {
    product: {}
  }
}
</script>

<template>
  <div class="product-card" @click="this.$router.push('product/' + product.id)">
      <img :src="'http://' + product.image" alt="">
      <div class="title">{{ product.title }}</div>
      <div class="short-desc">{{ product.shortDescription }}</div>
      <div class="price">{{ product.price }} Р</div>
    <button class="btn buy">Купить</button>
  </div>
</template>

<style scoped>

.product-card {
  width: 210px;
  height: 413px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  transition: .3s;
}

.product-card:hover {
  transform: translateY(-15px);
  cursor: pointer;
}

img {
  width: 100%; 
  height: 258px;
  object-fit: cover;
  border-radius: 8px;
}

.title {
  font-size: 18px;
  font-weight: 500;
  margin-top: 10px;
}

.short-desc {
  font-size: 14px;
  margin-top: 10px
}

.price {
  font-size: 18px;
  margin-top: 10px;
  color: #373f50;
  font-weight: 600;
}

.btn {
  border-radius: 4px;
  margin-top: 10px;
  width: 100px;
  height: 35px;
  margin-right: 5px;
  background-color: #fe696a;
  color: #fff;
}
</style>