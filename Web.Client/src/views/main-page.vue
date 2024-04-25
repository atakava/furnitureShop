<script>
import SliderElement from "@/components/slider-element.vue";
import ProductGrid from "@/components/product-grid.vue";
import FooterElement from "@/components/footer-element.vue";
import Callback from "@/components/callback.vue";

export default {
  name: "main-page",
  components: {Callback, FooterElement, ProductGrid, SliderElement},
}
</script>

<template>
  <section class="wrap">
    <slider-element/>
    <product-grid/>
    <callback/>
  </section>
</template>

<style scoped>

</style>