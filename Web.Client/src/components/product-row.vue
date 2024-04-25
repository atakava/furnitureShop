<script>
import ProductCard from "@/components/product-card.vue";

export default {
  name: "product-row",
  components: {ProductCard},
  props: {
    data: Array
  },
  created() {
  },
  data() {
    return {
      products: []
    }
  },
  methods: {}
}
</script>

<template>
  <product-card :product="item" v-for="item in data" :key="item.id"></product-card>
</template>

<style scoped>
.product-row {
  margin: 75px 0 75px 0;
}

.title {
  font-size: 24px;
  font-weight: 500;
  cursor: pointer;
  margin-bottom: 25px;
}

.products {
  display: flex;
  justify-content: space-between;
}
</style>