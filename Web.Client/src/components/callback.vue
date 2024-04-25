<template>
  <div class="wrap container">
    <div class="feedback-form" id="callback">
      <h2>Обратная связь</h2>
      <form @submit.prevent="createUser">
        <div class="form-group">
          <label for="name">Имя:</label>
          <input class="input" type="text" id="name" v-model="name" required/>
        </div>
        <div class="form-group">
          <label for="phone">Номер телефона:</label>
          <input class="input" type="tel" id="phone" v-model="phone" required/>
        </div>
        <div class="checkbox-form">
          <input type="checkbox" id="privacyPolicy" v-model="privacyPolicy" required/>
          <label for="privacyPolicy">Я соглашаюсь на передачу персональных данных согласно политике конфиденциальности и
            пользовательскому соглашению.</label>
        </div>
        <button type="submit">Отправить</button>
      </form>
    </div>

    <div v-if="showSuccessPopup" class="popup-overlay">
      <div class="popup-content">
        <p>Ваша заявка успешно отправлена и будет обработана в течение 24 часов</p>
        <button @click="closePopup">Закрыть</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: "callback",
  data() {
    return {
      name: "",
      phone: "",
      privacyPolicy: false,
      showSuccessPopup: false,
    };
  },
  methods: {
    async createUser() {
      try {
        const response = await axios.post(
            "http://localhost:5199/api/User/Create",
            {
              name: this.name,
              phone: this.phone
            },
            {
              withCredentials: true,
              headers: {
                'Content-Type': 'application/json'
              }
            }
        );

        console.log('Create catalog', response);
        console.log('Зарегаллся епта', response);

        this.name = "";
        this.phone = "";

        this.showSuccessPopup = true;

        setTimeout(() => {
          this.showSuccessPopup = false;
        }, 3000);

      } catch (e) {
        console.error("ошибка регистрации пользователя", e);
        throw e;
      }
    },
    closePopup() {
      this.showSuccessPopup = false;
    },
  },
};
</script>

<style scoped>
.wrap {
  position: relative;
}

.feedback-form {
  max-width: 1280px;
  width: 100%;
  margin: 50px auto;
  padding: 20px;
  background-color: #474E5E;
  border-radius: 5px;
}

h2 {
  color: #fff;
  margin: 20px 0;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  font-weight: bold;
  margin-bottom: 10px;
  color: #fff;
}

.input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
  margin-bottom: 15px;
}

.checkbox-form {
  display: flex;
  align-items: center;
  margin-bottom: 25px;
}

input[type="checkbox"] {
  margin-right: 10px;
}

input[type="checkbox"]:checked {
  background-color: #fe696a;
}

button {
  background-color: #fe696a;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
}

button:hover {
  background-color: #d45c5d;
}

.popup-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.popup-content {
  background: #fff;
  padding: 20px;
  text-align: center;
  border-radius: 5px;
}

.popup-content p {
  margin-bottom: 15px;
}
</style>
