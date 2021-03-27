<template>
  <v-card
    color="grey lighten-4"
    min-width="350px"
    flat
    >
    <v-toolbar
      color="deep-purple"
      dark
    ><v-toolbar-title v-html="selectedEvent.name"></v-toolbar-title>
      <v-spacer></v-spacer>
    </v-toolbar>
    <form>
      {{ new Date(treatmentSessionData.start).toLocaleString() }} <br>
      {{ new Date(treatmentSessionData.end).toLocaleString() }} <br>
      <v-btn
      type="button"
      color="success"
      @click="accept"
      >
      Accept
      </v-btn>
      <v-btn
      type="button"
      color="failure"
      @click="reject"
      >
      Reject
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

export default {
  data: () => ({
    treatmentSessionData: {},
  }),
  props: {
    selectedEvent: {
      referenceId: {
        type: String,
        required: true,
      },
    }
  },
  methods: {
    ...mapGetters([
      'treatmentSession'
    ]),
    ...mapActions([
      'getTreatmentSession',
      'updateTreatmentSession'
    ]),
    toggleEditForm(hasChanges) {
      this.$emit('toggleEditForm', hasChanges);
    },
    async accept() {
    //   console.log('accept');
      this.treatmentSessionData.status = 'Accepted';
      await this.updateTreatmentSession(this.treatmentSessionData);
      this.toggleEditForm(true);
    },
    async reject() {
    //   console.log('reject');
      this.treatmentSessionData.status = 'Rejected';
      await this.updateTreatmentSession(this.treatmentSessionData);
      this.toggleEditForm(true);
    }
  },
  async created() {
    await this.getTreatmentSession(this.selectedEvent.referenceId);
    this.treatmentSessionData = await this.treatmentSession();
  }
}
</script>