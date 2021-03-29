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
        v-if="isPatientRole()"
        :disabled="false"
        color="primary"
        class="ma-2 white--text"
        @click="() => toggleAddForm(false)"
      >
        Add
        <v-icon
          right
          dark
        >
          mdi-plus
        </v-icon>
      </v-btn>
      <v-btn
        icon
        class="ma-2"
        @click="$refs.calendar.next()"
      >
        <v-icon>mdi-chevron-right</v-icon>
      </v-btn>
    </v-sheet>
    <v-sheet height="600">
      <v-menu
          v-if="isPatientRole()"
          v-model="selectedAddOpen"
          :close-on-content-click="false"
          :activator="selectedAddElement"
          offset-x
        >
          <AddTreatmentSession @toggleAddForm="toggleAddForm" />
      </v-menu>
      <v-calendar
        ref="calendar"
        v-model="value"
        :weekdays="weekday"
        :type="type"
        :events="events"
        :event-overlap-mode="mode"
        :event-overlap-threshold="30"
        :event-color="getEventColor"
        @click:event="showEvent"
        @change="getEvents"
      ></v-calendar>
      <v-menu
          v-if="isPatientRole()"
          v-model="selectedOpen"
          :close-on-content-click="false"
          :activator="selectedElement"
          offset-x
        >
          <EditTreatmentSession @toggleEditForm="toggleEditForm" :selectedEvent="selectedEvent" />
      </v-menu>
      <v-menu
          v-else
          v-model="selectedResolveOpen"
          :close-on-content-click="false"
          :activator="selectedResolveElement"
          offset-x
        >
          <ResolveTreatmentSession @toggleEditForm="toggleResolveForm" :selectedEvent="selectedEvent" />
      </v-menu>
    </v-sheet>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import AddTreatmentSession from '../components/AddTreatmentSession'
import EditTreatmentSession from '../components/EditTreatmentSession'
import ResolveTreatmentSession from '../components/ResolveTreatmentSession'

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
    selectedEvent: {},
    selectedElement: null,
    selectedAddElement: null,
    selectedResolveElement: null,
    selectedOpen: false,
    selectedAddOpen: false,
    selectedResolveOpen: false,
    tokenData: {}
  }),
  components: {
    AddTreatmentSession,
    EditTreatmentSession,
    ResolveTreatmentSession
  },
  methods: {
    ...mapGetters([
        "authTokenData",
        "allTreatmentSessions"
    ]),
    ...mapActions([
        "getPatientTreatmentSessions",
        "getDentalTeamTreatmentSessions"
    ]),
    isPatientRole() {
      return this.tokenData.role == 'Patient';
    },
    async loadTokenData() {
      this.tokenData = await this.authTokenData();
    },
    async loadTreatmentSessions() {
        if (this.tokenData.role == 'Patient') {
            await this.getPatientTreatmentSessions(this.tokenData.patientReferenceId);
        } else {
            await this.getDentalTeamTreatmentSessions(this.tokenData.dentalTeamReferenceId);
        }
    },
    async toggleResolveForm(hasChanges) {
      this.selectedResolveOpen = !this.selectedResolveOpen;
      if (hasChanges) {
        await this.loadTreatmentSessions();
        this.getFilteredEvents();
      }
    },
    async toggleAddForm(hasChanges) {
      this.selectedAddOpen = !this.selectedAddOpen;
      if (hasChanges) {
        await this.loadTreatmentSessions();
        this.getFilteredEvents();
      }
    },
    async toggleEditForm(hasChanges) {
      this.selectedOpen = !this.selectedOpen;
      if (hasChanges) {
        await this.loadTreatmentSessions();
        this.getFilteredEvents();
      }
    },
    showEvent ({ nativeEvent, event }) {
      if (this.tokenData.role == 'Patient') {
        this.showEditEvent({ nativeEvent, event });
      } else {
        this.showResolveEvent({ nativeEvent, event });
      }
    },
    showEditEvent ({ nativeEvent, event }) {
      const open = () => {
        this.selectedEvent = event
        this.selectedElement = nativeEvent.target
        setTimeout(() => {
          this.selectedOpen = true
        }, 10)
      }

      if (this.selectedOpen) {
        this.selectedOpen = false
        setTimeout(open, 10)
      } else {
        open()
      }

      nativeEvent.stopPropagation()
    },
    showResolveEvent ({ nativeEvent, event }) {
      const open = () => {
        this.selectedEvent = event
        this.selectedResolveElement = nativeEvent.target
        setTimeout(() => {
          this.selectedResolveOpen = true
        }, 10)
      }

      if (this.selectedResolveOpen) {
        this.selectedResolveOpen = false
        setTimeout(open, 10)
      } else {
        open()
      }

      nativeEvent.stopPropagation()
    },
    getFilteredEvents(filter = (ts) => ts) {
      const treatmentSessions = this.allTreatmentSessions();
      this.events = treatmentSessions
        .filter(filter)
        .map(ts => {
            return {
            referenceId: ts.ReferenceId,
            patientReferenceId: ts.PatientReferenceId,
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
      await this.loadTokenData();
      await this.loadTreatmentSessions();
      this.getFilteredEvents();
  }
}
</script>