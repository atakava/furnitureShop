<script>
import axios from 'axios';

export default {
  name: "slider-element",
  data() {
    return {
      slider: null,
      transform: 'translateX(',
      width: 850,
      current: 0,
      timer: 0,
      catalog: [
        {
          title: "Диваны",
          img: "/item/item 4.webp",
          path: ""
        },
        {
          title: "Кресла",
          img: "/item/item 4.webp",
          path: ""
        },
        {
          title: "Стулья и Столы",
          img: "/item/item 4.webp",
          path: ""
        },
        {
          title: "Кровати",
          img: "/item/item 4.webp",
          path: "/catalog"
        },
      ]
    }
  },
  async created() {
    this.play();
    await this.fetchSlides();
  },
  methods: {
    async fetchSlides() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/Slider/GetAll",
            null,
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        this.slider = response.data.data;

      } catch (e) {
        console.error("ошибка получения слайдов", e);
        throw e;
      }
    },
    nextSlide: function () {
      this.current++;
      if (this.current >= this.slider.length) {
        this.current = 0;
      }
      this.resetPlay();
    },
    prevSlide: function () {
      this.current--;
      if (this.current < 0) {
        this.current = this.slider.length - 1;
      }
      this.resetPlay();
    },
    selectSlide: function (i) {
      this.current = i;
      this.resetPlay();
    },
    resetPlay: function () {
      clearInterval(this.timer);
      this.play();
    },
    play: function () {
      let app = this;
      this.timer = setInterval(function () {
        app.nextSlide();
      }, 6000)
    }
  }
}
</script>

<template>
  <div class="content" v-if="slider">
    <div class="slider">
      <div class="content-slide" :style="{ transform: this.transform + -width * current + 'px)' }">
        <img class="slide-item" alt="slider item" :src="'http://' + item.imagePath" v-for="item in slider"/>
      </div>
      <button class="btn left" @click="prevSlide">&lt;</button>
      <button class="btn right" @click="nextSlide ">&gt;</button>
      <div class="bullets">
        <div v-for="(slide, i) in this.slider" @click="selectSlide(i)"
             v-html="i == current ? '&#9679;' : '&omicron;'">
        </div>
      </div>
    </div>
    <div class="recommended">
      <div class="recommended-block" v-for="item in catalog" @click="this.$router.push('/catalog')">
        <p class="text-item">{{ item.title }}</p>
        <img class="img-item" :src="item.img" alt="item 1">
      </div>
    </div>
  </div>
</template>

<style scoped>
.content {
  margin: 50px 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.slider {
  width: 850px;
  height: 430px;
  border-radius: 20px;
  overflow: hidden;
  position: relative;
}

.content-slide {
  display: flex;
  transition: .3s;
}

.slide-item {
  min-width: 850px;
  width: 850px;
  height: 430px;
}

.btn {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  width: 40px;
  height: 40px;
  border-radius: 50%;
  cursor: pointer;
  background-color: #ffffffA6;
}

.left {
  left: 5%;
}

.right {
  right: 5%;
}

.bullets {
  display: flex;
  position: absolute;
  bottom: 6%;
  right: 6%;
}

.bullets div {
  color: #fff;
  cursor: pointer;
  margin: 0 3px;
}

.recommended {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  column-gap: 10px;
  row-gap: 10px;
}

.recommended-block {
  margin: 5px 0;
  border-radius: 20px;
  width: 200px;
  height: 200px;
  position: relative;
  cursor: pointer;
}

.text-item {
  position: absolute;
  bottom: 5%;
  width: 100%;
  text-align: center;
  color: #fff;
  cursor: pointer;
  font-size: 16px;
}

.recommended-block:hover .text-item {
  color: #fbffcc;
}

.img-item {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border-radius: 20px;
  z-index: -1;
}
</style>