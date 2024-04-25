<script>
import HeaderElement from "@/components/header-element.vue";
import FooterElement from "@/components/footer-element.vue";

export default {
  name: "main-layout",
  components: {FooterElement, HeaderElement},
}
</script>

<template>
  <header-element/>
  <main class="container">
    <slot></slot>
  </main>
  <footer-element/>
</template>

<style scoped>

</style>