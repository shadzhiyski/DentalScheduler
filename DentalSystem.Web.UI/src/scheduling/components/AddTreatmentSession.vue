<template>
<v-card
    color="grey lighten-4"
    min-width="350px"
    flat
    >
    <v-toolbar
      :color="color"
      dark
    >
      <v-btn icon>
      <v-icon>mdi-pencil</v-icon>
      </v-btn>
      <v-toolbar-title v-html="name"></v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon>
      <v-icon>mdi-heart</v-icon>
      </v-btn>
      <v-btn icon>
      <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
    </v-toolbar>
    <form @submit.prevent="submit">
      <v-select
        v-model="selectedTreatmentReferenceId"
        :items="treatments"
        item-text="Name"
        item-value="ReferenceId"
        label="Treatment"
        solo
      ></v-select>
      <v-select
        v-model="selectedDentalTeamReferenceId"
        :items="dentalTeams"
        item-text="Name"
        item-value="ReferenceId"
        label="Dental Team"
        solo
      ></v-select>
      <DateTimePicker
        @dateChanged="updateStartDate"
        @timeChanged="updateStartTime"
        label="Start Date"
        :date="treatmentSessionData.startDate"
        :time="treatmentSessionData.startTime" />
      <v-slider
        v-model="duration"
        :rules="rules.age"
        color="green"
        label="Minutes"
        hint="Be honest"
        min="20"
        max="120"
        step="5"
        thumb-label="always"
      ></v-slider>

      <v-btn
      type="submit"
      color="success"
      >
      Request
      </v-btn>
    </form>
    <v-card-actions>
      <v-btn
      text
      color="secondary"
      @click="() => toggleEditForm(false)"
      >
      Cancel
      </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import DateTimePicker from '../../common/components/DateTimePicker'

export default {
  data: () => ({
    treatmentSessionData: {
      treatmentReferenceId: null,
      dentalTeamReferenceId: null,
      startDate: new Date().toISOString().substr(0, 10),
      startTime: new Date().toISOString().substr(11, 5),
      status: 'Requested'
    },
    treatments: [],
    selectedTreatmentReferenceId: {},
    dentalTeams: [],
    selectedDentalTeamReferenceId: {},
    duration: 30,
    rules: {
      age: [
        val => val < 90 || `Long sessions might not get approved`,
      ],
    },
    name: 'Add Treatment Session',
    color: 'green'
  }),
  components: {
    DateTimePicker
  },
  methods: {
    ...mapGetters([
      'allTreatments',
      'allDentalTeams',
      'authTokenData'
    ]),
    ...mapActions([
      'createTreatmentSession',
      'getTreatments',
      'getDentalTeams'
    ]),
    updateStartDate(startDate) {
      this.treatmentSessionData.startDate = startDate;
    },
    updateStartTime(startTime) {
      this.treatmentSessionData.startTime = startTime;
    },
    toggleEditForm(hasChanges) {
      this.$emit('toggleEditForm', hasChanges);
    },
    async submit() {
      console.log(this.selectedTreatment);
      this.treatmentSessionData.treatmentReferenceId = this.selectedTreatmentReferenceId;
      this.treatmentSessionData.dentalTeamReferenceId = this.selectedDentalTeamReferenceId;
      this.treatmentSessionData.start = `${this.treatmentSessionData.startDate}T${this.treatmentSessionData.startTime}:00Z`;
      let endDate = new Date(this.treatmentSessionData.start);
      endDate.setMinutes(endDate.getMinutes() + this.duration);
      this.treatmentSessionData.end = endDate.toISOString();
      await this.createTreatmentSession(this.treatmentSessionData);
      this.toggleEditForm(true);
    }
  },
  async created() {
    this.treatmentSessionData.patientReferenceId = await this.authTokenData().patientReferenceId;
    await this.getTreatments();
    await this.getDentalTeams();

    this.treatments = await this.allTreatments();
    this.dentalTeams = await this.allDentalTeams();

    this.selectedTreatmentReferenceId = this.treatments[0];
    this.selectedDentalTeamReferenceId = this.dentalTeams[0];
  }
}
</script>