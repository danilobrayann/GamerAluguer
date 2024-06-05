<template>
  <div>
    <h1>Lista de Jogos</h1>
    <ul>
      <li v-for="game in games" :key="game.id">
        <div>
          <strong>Nome:</strong> {{ game.name }}<br>
          <strong>Valor:</strong> {{ game.valor }}<br>
          <strong>Valor de Aluguel:</strong> {{ game.valorAluguel }}<br>
          <strong>Gênero:</strong> {{ game.genero }}<br>
          <strong>Tipo de Mídia:</strong> {{ game.tipoDeMidia }}<br>
        </div>
        <button @click="deleteGame(game.id)">Deletar</button>
        <button @click="editGame(game)">Editar</button>
      </li>
    </ul>
    <h2>{{ selectedGame.id ? 'Editar Jogo' : 'Criar Novo Jogo' }}</h2>
    <form @submit.prevent="handleSubmit">
      <div>
        <label for="name">Nome:</label>
        <input type="text" v-model="form.name" required />
      </div>
      <div>
        <label for="valor">Valor:</label>
        <input type="number" v-model="form.valor" required />
      </div>
      <div>
        <label for="valorAluguel">Valor de Aluguel:</label>
        <input type="number" v-model="form.valorAluguel" required />
      </div>
      <div>
        <label for="genero">Gênero:</label>
        <input type="text" v-model="form.genero" required />
      </div>
      <div>
        <label for="tipoDeMidia">Tipo de Mídia:</label>
        <input type="text" v-model="form.tipoDeMidia" required />
      </div>
      <button type="submit">{{ selectedGame.id ? 'Atualizar' : 'Criar' }}</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useNuxtApp } from '#app';

interface Game {
  id: number;
  name: string;
  valor: number;
  valorAluguel: number;
  genero: string;
  tipoDeMidia: string;
}

const { $api } = useNuxtApp();
const games = ref<Game[]>([]);
const selectedGame = ref<Partial<Game>>({});
const form = ref<Partial<Game>>({
 
  name: '',
  valor: 0,
  valorAluguel: 0,
  genero: '',
  tipoDeMidia: ''
});

const fetchGames = async () => {
  try {
    const { data } = await $api.get('https://localhost:7182/api/Games');
    games.value = data;
  } catch (error) {
    console.error('Erro ao carregar a lista de jogos:', error);
  }
};

const selectGame = (game: Game) => {
  selectedGame.value = game;
  form.value = { ...game };
};

const editGame = (game: Game) => {
  selectGame(game);
};

const handleSubmit = async () => {
  try {
    if (selectedGame.value.id) {
      const response = await $api.put(`https://localhost:7182/api/Games/${selectedGame.value.id}`, form.value);
      if (response.status === 204) {
        const updatedGame = { ...form.value, id: selectedGame.value.id };
        const index = games.value.findIndex(g => g.id === selectedGame.value.id);
        if (index !== -1) games.value[index] = updatedGame;
        selectedGame.value = updatedGame; // Atualizar o jogo selecionado com os novos dados
      }
    } else {
      const response = await $api.post('https://localhost:7182/api/Games', form.value);
      if (response.status === 201) {
        games.value.push(response.data);
      }
    }
    form.value = {
     
      name: '',
      valor: 0,
      valorAluguel: 0,
      genero: '',
      tipoDeMidia: ''
    };
  } catch (error) {
    if (error.response && error.response.status === 404) {
      alert("Jogo não encontrado.");
    } else {
      console.error('Erro ao criar ou atualizar o jogo:', error);
    }
  }
};

const deleteGame = async (id: number) => {
  try {
    if (id) {
      const response = await $api.delete(`https://localhost:7182/api/Games/${id}`);
      if (response.status === 204) {
        games.value = games.value.filter(game => game.id !== id);
      }
    }
  } catch (error) {
    if (error.response && error.response.status === 404) {
      alert("Jogo não encontrado.");
    } else {
      console.error('Erro ao excluir o jogo:', error);
    }
  }
};

onMounted(() => {
  fetchGames();
});
</script>

<style>
/* Adicione qualquer estilo necessário aqui */
</style>
