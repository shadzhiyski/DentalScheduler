<template>
  <div>
  <v-menu
      v-model="showDatePicker"
      :close-on-content-click="false"
      :nudge-right="40"
      transition="scale-transition"
      offset-y
      min-width="auto"
      >
      <template v-slot:activator="{ on, attrs }">
      <v-text-field
      v-model="dateEdit"
      :label="label"
      prepend-icon="mdi-calendar"
      readonly
      v-bind="attrs"
      v-on="on"
      ></v-text-field>
      </template>
      <v-date-picker
      v-model="dateEdit"
      @input="showDatePicker = false"
      @click:date="saveDate"
      ></v-date-picker>
  </v-menu>
  <v-menu
      ref="menu"
      v-model="showTimePicker"
      :close-on-content-click="false"
      :nudge-right="40"
      :return-value.sync="timeEdit"
      transition="scale-transition"
      offset-y
      max-width="290px"
      min-width="290px"
      >
      <template v-slot:activator="{ on, attrs }">
          <v-text-field
          v-model="timeEdit"
          label="Picker in menu"
          prepend-icon="mdi-clock-time-four-outline"
          readonly
          v-bind="attrs"
          v-on="on"
          ></v-text-field>
      </template>
      <v-time-picker
          v-if="showTimePicker"
          v-model="timeEdit"
          full-width
          @click:minute="saveTime"
      ></v-time-picker>
  </v-menu>
  </div>
</template>

<script>
export default {
  data: () => ({
    dateEdit: new Date().toISOString().substr(0, 10),
    timeEdit: "00:00",
    showDatePicker: false,
    showTimePicker: false,
  }),
  props: {
    label: {
      type: String,
      required: true,
    },
    date: {
      type: String,
      required: true
    },
    time: {
      type: String,
      required: true
    },
  },
  methods: {
    saveDate() {
      this.$emit("dateChanged", this.dateEdit);
    },
    saveTime() {
      this.$refs.menu.save(this.timeEdit);
      this.$emit("timeChanged", this.timeEdit);
    }
  },
  created() {
    this.dateEdit = this.date;
    this.timeEdit = this.time;
  }
}
</script>