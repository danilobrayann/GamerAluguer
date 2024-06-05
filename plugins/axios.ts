
import axios, { type AxiosInstance } from 'axios';
import { defineNuxtPlugin } from '#app';

export default defineNuxtPlugin((nuxtApp) => {
  const api: AxiosInstance = axios.create({
    baseURL: 'https://localhost:7182/api/Games',  // Certifique-se de que esta URL está correta
  });

  nuxtApp.provide('api', api);
});


/*

import axios, { AxiosInstance } from 'axios';
import { defineNuxtPlugin } from '#app';

export default defineNuxtPlugin((nuxtApp) => {
  const api: AxiosInstance = axios.create({
    baseURL: 'https://localhost:7182/api/Games',  // Certifique-se de que esta URL está correta
  });

  nuxtApp.provide('api', api);
});



*/ 