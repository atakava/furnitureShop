<script>
import axios from "axios";

export default {
  name: "product-page",
  data() {
    return {
      productId: null,
      product: null
    }
  },
  methods: {
    async fetchProduct() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Product/Get",
            {id: this.productId},
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            });

        console.log(response.data);

        this.product = response.data;

      } catch (e) {
        console.error("ошибка получения товара", e);
        throw e;
      }
    }
  },
  async created() {
    this.productId = this.$route.params.id;
    await this.fetchProduct();
  },
}
</script>

<template>
  <section class="wrap" v-if="product">
    <div class="top">
      <img class="product-image" :src="'http://' + product.image" alt="product image">
      <div class="content">
        <div class="title">{{ product.title }}</div>
        <div class="parameters">
          <div class="parameter" v-for="item in product.productParameters">
            <div class="name">{{ item.name }}:</div>
            <div class="value">{{ item.value }}</div>
          </div>
        </div>
      </div>
      <div class="buy-column">
        <div class="panel-buy">
          <div class="price">Цена: {{ product.price }}</div>
          <div class="btn-panel">
            <button class="btn buy">Купить</button>
          </div>
          <div class="stoke">
            <img src="">
            <div class="">В наличии</div>
          </div>
        </div>

        <p class="bot">Цена на сайте может отличаться от цены в магазинах сети. Внешний вид книги может отличаться от
          изображения на
          сайте.</p>
      </div>
    </div>
    <div class="desc">
      {{ product.description }}
    </div>
  </section>
</template>

<style scoped>
.wrap {
  margin: 50px auto;
}

.top {
  display: flex;
  justify-content: space-between;
}

.product-image {
  width: 40%;
  /* height: 500px; */
  border-radius: 20px;
}

.content {
  width: 40%;
  padding: 0 40px;
}

.title {
  font-size: 28px;
}

.parameter {
  display: flex;
  justify-content: space-between;
  margin: 10px 0;
}

.buy-column {
  width: 30%;
}

.panel-buy {
  border-radius: 14px;
  padding: 20px;
  color: #fff;
  width: 100%;
  background: #373f50A6;
}

.btn-panel {
  display: flex;
  margin-top: 14px;
}

.btn {
  height: 35px;
  width: 150px;
}

.buy {
  background-color: #fe696a;
  color: #fff;
  font-weight: 500;
  margin-right: 10px;
}

.price {
  font-size: 30px;
}

.bot {
  margin-top: 15px;
  color: #474E5E;
  font-size: 14px;
}

.desc {
  margin-top: 25px;
  font-size: 18px;
}

</style>