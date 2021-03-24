<template>
  <div>
    <v-sheet
      tile
      height="54"
      class="d-flex"
    >
      <v-btn
        icon
        class="ma-2"
        @click="$refs.calendar.prev()"
      >
        <v-icon>mdi-chevron-left</v-icon>
      </v-btn>
      <v-select
        v-model="type"
        :items="types"
        dense
        outlined
        hide-details
        class="ma-2"
        label="type"
      ></v-select>
      <v-select
        v-model="mode"
        :items="modes"
        dense
        outlined
        hide-details
        label="event-overlap-mode"
        class="ma-2"
      ></v-select>
      <v-select
        v-model="weekday"
        :items="weekdays"
        dense
        outlined
        hide-details
        label="weekdays"
        class="ma-2"
      ></v-select>
      <v-spacer></v-spacer>
      <v-btn
        icon
        class="ma-2"
        @click="$refs.calendar.next()"
      >
        <v-icon>mdi-chevron-right</v-icon>
      </v-btn>
    </v-sheet>
    <v-sheet height="600">
      <v-calendar
        ref="calendar"
        v-model="value"
        :weekdays="weekday"
        :type="type"
        :events="events"
        :event-overlap-mode="mode"
        :event-overlap-threshold="30"
        :event-color="getEventColor"
        @change="getEvents"
      ></v-calendar>
    </v-sheet>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  data: () => ({
    type: 'month',
    types: ['month', 'week', 'day', '4day'],
    mode: 'stack',
    modes: ['stack', 'column'],
    weekday: [0, 1, 2, 3, 4, 5, 6],
    weekdays: [
      { text: 'Sun - Sat', value: [0, 1, 2, 3, 4, 5, 6] },
      { text: 'Mon - Sun', value: [1, 2, 3, 4, 5, 6, 0] },
      { text: 'Mon - Fri', value: [1, 2, 3, 4, 5] },
      { text: 'Mon, Wed, Fri', value: [1, 3, 5] },
    ],
    value: '',
    events: [],
    colors: {
        Requested: 'orange',
        Accepted: 'green',
        Rejected: 'red'
    },
  }),
  methods: {
    ...mapGetters([
        "authTokenData",
        "allTreatmentSessions"
    ]),
    ...mapActions([
        "getPatientTreatmentSessions",
        "getDentalTeamTreatmentSessions"
    ]),
    async loadTreatmentSessions() {
        var tokenData = await this.authTokenData();

        if (tokenData.role == 'Patient') {
            await this.getPatientTreatmentSessions(tokenData.patientReferenceId);
        } else {
            await this.getDentalTeamTreatmentSessions(tokenData.dentalTeamReferenceId);
        }
    },
    getFilteredEvents(filter = (ts) => ts) {
      const treatmentSessions = this.allTreatmentSessions();
      this.events = treatmentSessions
        .filter(filter)
        .map(ts => {
            return {
            name: ts.Treatment.Name,
            start: new Date(ts.Start),
            end: new Date(ts.End),
            color: this.colors[ts.Status],
            timed: false
            };
        });
    },
    async getEvents ({ start, end }) {
      const min = `${start.date}T00:00:00`
      const max = `${end.date}T23:59:59`
      this.getFilteredEvents(ts => ts.Start >= min && ts.End <= max);
    },
    getEventColor (event) {
      return event.color
    },
  },
  async created() {
      await this.loadTreatmentSessions();
      this.getFilteredEvents();
  }
}
</script>