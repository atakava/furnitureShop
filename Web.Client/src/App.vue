<script setup>
import {RouterView} from 'vue-router'
</script>

<template>
  <component :is="$route.meta.layout">
    <router-view/>
  </component>
</template>

<style scoped>

</style>
