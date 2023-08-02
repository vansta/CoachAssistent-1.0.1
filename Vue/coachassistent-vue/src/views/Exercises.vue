<template>
  <v-container fluid>
    <v-row>
        <v-col>
            <v-text-field label="Search"></v-text-field>
        </v-col>
        <v-col>
            <v-autocomplete label="Type"></v-autocomplete>
        </v-col>
        <v-spacer></v-spacer>
        <v-col cols="1">
            <v-btn icon="mdi-plus" variant="text" color="blue" @click="editExercise = true"></v-btn>
        </v-col>
    </v-row>
    <v-row>
        <v-col v-for="(exercise, index) in exercises" :key="index">
            <v-card :title="exercise.name">
                <v-card-text>
                    {{ exercise.description }}
                </v-card-text>
            </v-card>
        </v-col>
    </v-row>
    <v-row>
        <v-col>
            <v-pagination :length="exercises.length"></v-pagination>
        </v-col>
    </v-row>
    
    <v-dialog v-model="editExercise">
        <edit-exercise></edit-exercise>
    </v-dialog>
    
  </v-container>
</template>

<script>
import { defineComponent } from 'vue';

import EditExercise from '../components/Exercises/Edit.vue';
export default defineComponent({
    components: {
        EditExercise
    },
    data () {
        return {
            editExercise: false,
            exercises: [
                { name: 'test' },
                { name: 'test' },
                { name: 'test' },
                { name: 'test' },
            ]
        }
    }
})
</script>

<style>

</style>