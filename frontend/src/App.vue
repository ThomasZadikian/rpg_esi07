<template>
  <v-app>
    <v-app-bar color="primary" prominent>
      <v-app-bar-nav-icon></v-app-bar-nav-icon>
      <v-toolbar-title>RPG_ESI07</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
        <v-icon>mdi-account</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container>
        <v-row>
          <v-col cols="12">
            <v-card>
              <v-card-title>
                <span class="text-h4">Bienvenue sur RPG_ESI07</span>
              </v-card-title>
              <v-card-text>
                <p class="text-subtitle-1">
                  Architecture sécurisée - RNCP 36286 Cybersécurité
                </p>
                <v-chip color="success" class="ma-2">
                  <v-icon start>mdi-check</v-icon>
                  Vue 3 + TypeScript
                </v-chip>
                <v-chip color="success" class="ma-2">
                  <v-icon start>mdi-check</v-icon>
                  Vuetify 3
                </v-chip>
                <v-chip color="success" class="ma-2">
                  <v-icon start>mdi-check</v-icon>
                  .NET 10
                </v-chip>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
// Script setup TypeScript
</script>