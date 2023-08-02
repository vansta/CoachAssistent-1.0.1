<template>
  <v-card>
    <v-card-title>
        <v-text-field v-model="model.title" label="Title"></v-text-field>
    </v-card-title>
    <v-card-text>
        <v-textarea v-model="model.description" label="Description"></v-textarea>
    </v-card-text>
  </v-card>
</template>

<script>
import { defineComponent } from 'vue';
export default defineComponent({
    name: "EditExercise",
    data () {
        return {
            model: {}
        }
    }
})
</script>

<style>

</style>